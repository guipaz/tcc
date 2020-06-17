using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Tiles : MonoBehaviour
{
    public GameObject container;
    public GameObject tilePrefab;

    int cursorPreviousTid = -1;

    public void SetAdd()
    {
        Global.cursorAdd = true;
        if (cursorPreviousTid != -1)
        {
            Global.cursorObject.GetComponent<Editor_Cursor>().SetTile(cursorPreviousTid);
        }
    }

    public void SetRmv()
    {
        Global.cursorAdd = false;
        
        cursorPreviousTid = Global.cursorObject.GetComponent<Editor_Cursor>().currentTid;

        Global.cursorObject.GetComponent<Editor_Cursor>().SetTile(-1);
    }

    public void SetupTiles()
    {
        foreach (Transform child in container.transform)
            Destroy(child.gameObject);

        var tid = 0;
        foreach (var tile in GameTileset.masterTileset.tiles)
        {
            var currentTid = tid;
            var obj = Instantiate(tilePrefab, container.transform);
            obj.GetComponent<Image>().sprite = tile;
            obj.GetComponent<PanelTile>().onClick = () =>
            {
                Global.cursorObject.GetComponent<Editor_Cursor>().SetTile(currentTid);
            };
            tid++;
        }
    }
}
