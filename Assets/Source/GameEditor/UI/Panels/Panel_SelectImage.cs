using System;
using Assets;
using Assets.Source;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SelectImage : MonoBehaviour, IEditorPanel
{
    public GameObject container;
    public GameObject tilesetContainer;
    public GameObject tilePrefab;
    public GameObject textureItemPrefab;

    public Action<Sprite> OnSelected;

    bool loadedTilesets;
    Sprite selected;

    public void Confirm()
    {
        OnSelected?.Invoke(selected);
        Global.master.ClosePanel(gameObject);
    }

    public void Close()
    {
        Global.master.ClosePanel(gameObject);
    }

    public void DialogOpened()
    {
        if (!loadedTilesets)
        {
            LoadTilesets();
        }
    }

    void LoadTilesets()
    {
        foreach (var tilesetKey in Global.tilesets.Keys)
        {
            var obj = Instantiate(textureItemPrefab, tilesetContainer.transform);
            obj.GetComponent<Text>().text = tilesetKey;
            obj.GetComponent<MapRecord>().OnSelected = record =>
            {
                ChangedTileset(tilesetKey);
            };
        }

        loadedTilesets = true;
    }

    void ChangedTileset(string name)
    {
        // destroy previous
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        // create current tiles
        foreach (var tile in Global.tilesets[name].tiles)
        {
            var obj = Instantiate(tilePrefab, container.transform);
            obj.GetComponent<Image>().sprite = tile;
            obj.GetComponent<PanelTile>().onClick = () =>
            {
                selected = tile;
            };
        }
    }
}