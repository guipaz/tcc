using Assets.Source;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Tiles : MonoBehaviour
{
    public GameObject container;
    public GameObject tilePrefab;

    Sprite cursorPreviousSprite;

    public void SetAdd()
    {
        Global.cursorAdd = true;
        if (cursorPreviousSprite != null)
            Global.cursorObject.GetComponent<SpriteRenderer>().sprite = cursorPreviousSprite;
    }

    public void SetRmv()
    {
        Global.cursorAdd = false;
        cursorPreviousSprite = Global.cursorObject.GetComponent<SpriteRenderer>().sprite;
        Global.cursorObject.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void SetupTiles()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        foreach (var tile in Global.currentMap.tileset.tiles)
        {
            var obj = Instantiate(tilePrefab, container.transform);
            obj.GetComponent<Image>().sprite = tile;
            obj.GetComponent<PanelTile>().onClick = () =>
            {
                Global.cursorObject.GetComponent<SpriteRenderer>().sprite = tile;
            };
        }
    }
}
