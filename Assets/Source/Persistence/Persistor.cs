using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

public class Persistor
{
    public static string DEFAULT_FOLDER = Application.dataPath + "/save/";

    public static Persistor instance { get; } = new Persistor();

    public IEnumerable<T> LoadFile<T>(string filename) where T : IPersistent
    {
        var content = File.ReadAllText(filename);

        var jobject = JSONDecoder.Decode(content);
        var supertypeName = jobject["supertype"].StringValue;
        var superType = Type.GetType(supertypeName);
        var dataList = jobject["data"].ArrayValue;

        if (superType == null)
        {
            Debug.LogError("No supertype informed in file");
            yield break;
        }

        foreach (var data in dataList)
        {
            var typeName = supertypeName;
            if (data.ObjectValue.TryGetValue("_TYPE", out var specificTypeName))
                typeName = specificTypeName.StringValue;

            var type = Type.GetType(typeName);
            if (type == null)
            {
                Debug.LogError("Could not find class for component '" + typeName + "'");
                yield break;
            }

            if (!typeof(T).IsAssignableFrom(type))
            {
                Debug.LogError("Type '" + typeName + "' is not T");
                yield break;
            }

            if (!superType.IsAssignableFrom(type))
            {
                Debug.LogError("Type '" + typeName + "' is not from file supertype '" + supertypeName + "'");
                yield break;
            }

            var _instance = (T) Activator.CreateInstance(type);
            _instance.SetData(new PersistenceData(data.ObjectValue));
            yield return _instance;
        }
    }

    public void SaveFile<T>(string filename, IEnumerable<IPersistent> dataList) where T : IPersistent
    {
        var superTypeName = typeof(T).FullName;
        var fileData = new List<JObject>();

        foreach (var obj in dataList)
        {
            var data = obj.GetData();
            fileData.Add(JObject.CreateObject(data.InternalData));
        }

        var finalData = new Dictionary<string, JObject>
        {
            ["supertype"] = JObject.CreateString(superTypeName), ["data"] = JObject.CreateArray(fileData)
        };
        var json = JSONEncoder.Encode(finalData);

        var file = new FileInfo(filename);
        file.Directory?.Create();
        File.WriteAllText(filename, json);
    }
}