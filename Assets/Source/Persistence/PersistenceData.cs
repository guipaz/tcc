using System;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;
public class PersistenceData
{
    Dictionary<string, JObject> _internalData;
    public Dictionary<string, JObject> InternalData => _internalData;

    public PersistenceData()
    {
        _internalData = new Dictionary<string, JObject>();
    }

    public PersistenceData(Dictionary<string, JObject> internalData)
    {
        this._internalData = internalData;
    }

    public List<string> GetKeys()
    {
        return InternalData.Keys.ToList();
    }

    public JObject Get(string key)
    {
        if (_internalData.ContainsKey(key))
            return _internalData[key];
        return null;
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        var obj = Get(key);
        if (obj == null)
            return defaultValue;

        if (typeof(T) == typeof(string))
            return (T) (object) obj.StringValue;
        if (typeof(T) == typeof(int))
            return (T)(object)obj.IntValue;
        if (typeof(T) == typeof(bool))
            return (T)(object)obj.BooleanValue;
        if (typeof(T) == typeof(PersistenceData))
            return (T)(object)new PersistenceData(obj.ObjectValue);
        if (typeof(T) == typeof(List<PersistenceData>))
        {
            var l = new List<PersistenceData>();
            foreach (var v in obj.ArrayValue)
            {
                l.Add(new PersistenceData(v.ObjectValue));
            }

            return (T)(object)l;
        }


        return (T)(object)obj;
    }

    public void Set<T>(string key, T obj)
    {
        if (obj == null)
            return;

        JObject jobject = null;
        if (typeof(T) == typeof(string) || typeof(T) == typeof(String))
            jobject = JObject.CreateString((string)(object)obj);
        if (typeof(T) == typeof(int))
            jobject = JObject.CreateInteger((int)(object)obj);
        if (typeof(T) == typeof(bool))
            jobject = JObject.CreateBoolean((bool)(object)obj);
        if (typeof(T) == typeof(PersistenceData))
            jobject = JObject.CreateObject(((PersistenceData)(object)obj).InternalData);
        if (typeof(T) == typeof(List<PersistenceData>))
        {
            var d = (List<PersistenceData>) (object) obj;
            var n = new List<JObject>();
            foreach (var v in d)
            {
                n.Add(JObject.CreateObject(v.InternalData));
            }

            jobject = JObject.CreateArray(n);
        }

        if (jobject != null)
            _internalData[key] = jobject;
        else
            Debug.LogError("Tried to set invalid persistence _internalData: " + key);
    }
}