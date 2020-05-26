using System;
using Assets;
using Assets.Source;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SelectImage : MonoBehaviour, IEditorPanel
{
    public GameObject container;
    public GameObject tilePrefab;

    public Action<Sprite> OnSelected;

    Sprite selected;

    public void Confirm()
    {
        OnSelected?.Invoke(selected);
        Global.master.ClosePanel("selectImage");
    }

    public void DialogOpened()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        foreach (var tile in Global.currentMap.tileset.tiles)
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