using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Editor_MasterController : MonoBehaviour
{
    public GameObject OverlayPanel;
    public GameObject MapsPanel;
    public GameObject EntityPanel;
    public GameObject SelectImagePanel;
    public GameObject SelectEventPanel;
    public GameObject CharacterPanel;
    public GameObject StatesPanel;
    public Sprite[] Tilesets;

    public void Awake()
    {
        Global.master = this;

        // slices the tilesets for use later
        Global.tilesets = new Dictionary<string, GameTileset>();
        foreach (var sprite in Tilesets)
        {
            Global.tilesets[sprite.name] = new GameTileset(sprite.name, sprite);
        }

        if (Global.loadGame != null)
        {
            Global.game = Persistor.instance.LoadFile<Game>(Persistor.DEFAULT_FOLDER + Global.loadGame).ToList()[0];
            Global.loadGame = null;
        }
    }

    public void RunGame()
    {
        CommitCurrentMap();

        // change scene
        SceneManager.LoadScene("PlayScene");
    }

    public void Save()
    {
        CommitCurrentMap();

        //TODO
        Persistor.instance.SaveFile<Game>(Persistor.DEFAULT_FOLDER + "save1.json", new [] { Global.game });
    }

    public void Exit()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void CommitCurrentMap()
    {
        // commits map changes
        GameObject.Find("Editor_Map").GetComponent<Editor_MapController>().Commit();
    }

    public void Start()
    {
        if (Global.game == null)
        {
            Global.game = new Game();
            Global.game.maps.Add(new GameMap(20, 20) { name = "Mapa 1" });
        }

        Global.currentMap = Global.game.maps[0];

        Global.canvasObject = GameObject.Find("Canvas");

        OverlayPanel?.SetActive(false);
        MapsPanel?.SetActive(false);
        EntityPanel?.SetActive(false);
        SelectImagePanel?.SetActive(false);
        SelectEventPanel?.SetActive(false);
        CharacterPanel?.SetActive(false);
        StatesPanel?.SetActive(false);

        Global.cursorObject = GameObject.Find("Editor_Cursor");

        GameObject.Find("Panel_Tiles").GetComponent<Panel_Tiles>().SetupTiles();

        LoadCurrentMap();
    }

    public void LoadCurrentMap()
    {
        GameObject.Find("Editor_Map").GetComponent<Editor_MapController>().GenerateCurrentMap();
        GameObject.Find("CurrentMapName").GetComponent<Text>().text = Global.currentMap.name ?? "Sem nome";
    }

    public void OpenPanelVoid(string name)
    {
        OpenPanel(name);
    }

    public GameObject OpenPanel(string name)
    {
        GameObject obj = null;

        if (name == "maps")
            obj = MapsPanel;
        else if (name == "entity")
            obj = EntityPanel;
        else if (name == "selectImage")
            obj = SelectImagePanel;
        else if (name == "selectEvent")
            obj = SelectEventPanel;
        else if (name == "character")
            obj = CharacterPanel;
        else if (name == "states")
            obj = StatesPanel;

        return OpenPanel(obj);
    }

    public GameObject OpenPanel(GameObject obj)
    {
        OverlayPanel?.SetActive(true);
        
        var parent = OverlayPanel?.transform?.parent;
        OverlayPanel?.transform?.SetParent(null);
        OverlayPanel?.transform?.SetParent(parent);

        obj?.SetActive(true);
        obj?.GetComponent<IEditorPanel>()?.DialogOpened();

        parent = obj?.transform.parent;
        obj?.transform?.SetParent(null);
        obj?.transform?.SetParent(parent);

        return obj;
    }

    public void ClosePanel(GameObject obj, bool destroy = false)
    {
        OverlayPanel?.SetActive(false);
        obj?.SetActive(false);

        if (destroy && obj != null)
        {
            Destroy(obj);
        }
    }
}