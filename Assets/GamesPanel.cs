using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GamesPanel : MonoBehaviour
{
    public GameObject contentObject;
    public GameObject recordPrefab;

    Action<string> onSelected;

    public void Open(Action<string> onSelected)
    {
        gameObject.SetActive(true);

        this.onSelected = onSelected;

        foreach (Transform t in contentObject.transform)
            Destroy(t.gameObject);

        var files = Directory.GetFiles(Persistor.DEFAULT_FOLDER);
        foreach (var f in files)
        {
            if (!f.EndsWith(".json"))
                continue;

            var fileName = f.Substring(f.LastIndexOf("/", StringComparison.Ordinal) + 1);
            var obj = Instantiate(recordPrefab, contentObject.transform);
            obj.GetComponent<Text>().text = fileName;
            var record = obj.GetComponent<MapRecord>();
            record.OnSelected = mapRecord =>
            {
                onSelected?.Invoke(fileName);
                gameObject.SetActive(false);
            };
        }
    }
}
