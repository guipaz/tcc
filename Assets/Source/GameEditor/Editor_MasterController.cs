using Assets;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor_MasterController : MonoBehaviour
{
    public GameObject OverlayPanel;
    public GameObject MapsPanel;
    public GameObject EntityPanel;
    public GameObject SelectImagePanel;
    public GameObject SelectEventPanel;

    public void Awake()
    {
        Global.master = this;
    }

    public void RunGame()
    {
        CommitCurrentMap();

        // change scene
        SceneManager.LoadScene("PlayScene");
    }

    public void CommitCurrentMap()
    {
        // commits map changes
        GameObject.Find("Editor_Map").GetComponent<Editor_MapController>().Commit();
    }

    public void Start()
    {
        Global.game = new Game();
        Global.currentMap = new GameMap(30, 20) { name = "Testando mapa" };
        Global.game.maps.Add(Global.currentMap);
        Global.canvasObject = GameObject.Find("Canvas");

        OverlayPanel?.SetActive(false);
        MapsPanel?.SetActive(false);
        EntityPanel?.SetActive(false);
        SelectImagePanel?.SetActive(false);
        SelectEventPanel?.SetActive(false);

        Global.cursorObject = GameObject.Find("Editor_Cursor");

        GameObject.Find("Panel_Tiles").GetComponent<Panel_Tiles>().SetupTiles();

        LoadCurrentMap();
    }

    public void LoadCurrentMap()
    {
        GameObject.Find("Editor_Map").GetComponent<Editor_MapController>().GenerateCurrentMap();
    }

    public void OpenPanelVoid(string name)
    {
        OpenPanel(name);
    }

    public GameObject OpenPanel(string name)
    {
        OverlayPanel?.SetActive(true);

        GameObject obj = null;

        if (name == "maps")
            obj = MapsPanel;
        else if (name == "entity")
            obj = EntityPanel;
        else if (name == "selectImage")
            obj = SelectImagePanel;
        else if (name == "selectEvent")
            obj = SelectEventPanel;

        obj?.SetActive(true);
        obj?.GetComponent<IEditorPanel>()?.DialogOpened();

        return obj;
    }

    public void ClosePanel(GameObject obj)
    {
        OverlayPanel?.SetActive(false);
        obj?.SetActive(false);
    }

    public void ClosePanel(string name)
    {
        GameObject obj = null;

        if (name == "maps")
            obj = MapsPanel;
        else if (name == "entity")
            obj = EntityPanel;
        else if (name == "selectImage")
            obj = SelectImagePanel;

        ClosePanel(obj);
    }
}