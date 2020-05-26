using Assets;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Maps : MonoBehaviour, IEditorPanel
{
    public GameObject mapRecordPrefab;
    public GameObject content;

    public InputField nameField;
    public InputField widthField;
    public InputField heightField;

    //TODO tileset

    GameMap selectedMap;

    public void DialogOpened()
    {
        Reload();
    }

    void Reload()
    {
        foreach (Transform child in content.transform)
            Destroy(child.gameObject);

        ClearFields();

        foreach (var map in Global.game.maps)
        {
            var obj = Instantiate(mapRecordPrefab, content.transform);
            obj.GetComponent<MapRecord>().gameMap = map;
            obj.GetComponent<MapRecord>().OnSelected = record =>
            {
                nameField.text = map.name;
                widthField.text = map.width.ToString();
                heightField.text = map.height.ToString();
                selectedMap = map;
            };
            obj.GetComponent<Text>().text = map.name;
        }
    }

    void ClearFields()
    {
        nameField.text = "";
        widthField.text = "";
        heightField.text = "";
    }

    public void New()
    {
        selectedMap = null;
        ClearFields();
    }

    public void Delete()
    {
        if (selectedMap != null && Global.game.maps.Count > 1)
        {
            Global.game.maps.Remove(selectedMap);
            selectedMap = null;
            Reload();
        }
    }

    public void Save()
    {
        if (selectedMap == null)
        {
            int.TryParse(widthField.text, out var width);
            int.TryParse(heightField.text, out var height);

            if (width == 0)
                width = 10;
            if (height == 0)
                height = 10;

            selectedMap = new GameMap(width, height);
        }

        selectedMap.name = nameField.text;
        Global.game.maps.Add(selectedMap);

        selectedMap = null;

        Reload();
    }

    public void SetCurrentMap()
    {
        if (selectedMap != null)
        {
            if (Global.currentMap != null)
            {
                Global.master.CommitCurrentMap();
            }

            Global.currentMap = selectedMap;
            Global.master.LoadCurrentMap();
            Global.master.ClosePanel(gameObject);
        }
    }
}
