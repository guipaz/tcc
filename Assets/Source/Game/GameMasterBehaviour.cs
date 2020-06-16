using Assets.Source;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameState = Assets.Source.Game.GameState;

public class GameMasterBehaviour : MonoBehaviour
{
    public static GameMasterBehaviour main;

    public PlayerBehaviour player;
    public GameObject entityPrefab;

    GameObject entitiesLayer;

    void Start()
    {
        main = this;

        GameState.main.Clear();

        MessagePanel.main.Toggle(false);

        player.GetComponent<SpriteRenderer>().sprite = Global.game.player.sprite;

        var startingMap = Global.game.player.startingMap;
        var startingX = Global.game.player.startingX;
        var startingY = Global.game.player.startingY;

        //TODO set first map and position
        GameState.main.ChangeMap(startingMap, startingX, startingY);
    }

    public GameObject GetEntityObject(string id)
    {
        var entities = GameObject.Find("Entities");
        foreach (Transform transform in entities.transform)
        {
            var obj = transform.gameObject;
            if (obj.name == id)
            {
                return obj;
            }
        }

        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("EditorScene");
            return;
        }

        if (GameState.main.currentExecutedEntity != null)
        {
            if (GameState.main.currentExecutedEntity.currentState.events.Count > GameState.main.currentExecutedEventIndex)
            {
                var currentEvent =
                    GameState.main.currentExecutedEntity.currentState.events[GameState.main.currentExecutedEventIndex];

                if (!currentEvent.startedExecution)
                {
                    currentEvent.Execute();
                    currentEvent.startedExecution = true;
                }
                else if (!currentEvent.finishedExecution)
                {
                    currentEvent.Update();
                }
                else
                {
                    GameState.main.currentExecutedEventIndex++;
                }
            }
            else
            {
                GameState.main.currentExecutedEntity = null;
            }
        }
    }

    public void InstantiateMap(GameMap map)
    {
        var mapObject = GameObject.Find("Map");
        foreach (Transform child in mapObject.transform)
            Destroy(child.gameObject);

        InstantiateLayer(mapObject, map, Layers.Terrain, map.terrainLayer, 2);
        InstantiateLayer(mapObject, map, Layers.Construction, map.constructionLayer, 1);
        InstantiateLayer(mapObject, map, Layers.Above, map.aboveLayer, -1);

        entitiesLayer = new GameObject(Layers.Entities.ToString());
        entitiesLayer.transform.parent = mapObject.transform;

        InstantiateEntities(map);
    }

    public void InstantiateEntities(GameMap map)
    {
        foreach (Transform child in entitiesLayer.transform)
            Destroy(child.gameObject);

        GameState.main.currentEntityBehaviours.Clear();

        foreach (var entity in map.entities)
        {
            var entityObject = Instantiate(entityPrefab, entitiesLayer.transform);
            entityObject.name = string.IsNullOrEmpty(entity.name) ? "Entity" : entity.name;
            entityObject.transform.localPosition = new Vector3(entity.location.x, entity.location.y, 0);

            var entityBehaviour = entityObject.GetComponent<EntityBehaviour>();
            entityBehaviour.gameEntity = entity;

            ProcessState(entityBehaviour);

            entityObject.GetComponent<SpriteRenderer>().sprite = entityBehaviour.currentState.image;

            GameState.main.currentEntityBehaviours.Add(entityBehaviour);
        }
    }

    void ProcessState(EntityBehaviour entityBehaviour)
    {
        foreach (var key in entityBehaviour.gameEntity.states.Keys)
        {
            var state = entityBehaviour.gameEntity.states[key];

            // sets the default first that won't have any conditions
            if (key == GameEntityState.DEFAULT_STATE_NAME)
            {
                entityBehaviour.currentState = state;
                continue;
            }

            // checks switch first
            if (state.switchCheck)
            {
                // didn't pass, go to the next
                if (!GameState.main.switches.ContainsKey(state.switchCheckName) || GameState.main.switches[state.switchCheckName] != state.switchCheckValue)
                {
                    continue;
                }
            }

            // checks variable
            if (state.variableCheck)
            {
                // didn't pass, go to the next
                if (!GameState.main.variables.ContainsKey(state.variableCheckName) || GameState.main.variables[state.variableCheckName] != state.variableCheckValue)
                {
                    continue;
                }
            }

            // all passed, this is the current state
            entityBehaviour.currentState = state;
            break;
        }
    }

    void InstantiateLayer(GameObject mapObject, GameMap map, Layers layerType, GameMapTileLayer layer, int depth)
    {
        var layerObject = new GameObject(layerType.ToString());
        layerObject.transform.parent = mapObject.transform;
        layerObject.transform.localPosition = new Vector3(0, 0, depth);

        for (var y = 0; y < map.height; y++)
        {
            for (var x = 0; x < map.width; x++)
            {
                var tid = layer.tids[x, y];
                if (tid > -1)
                {
                    var obj = new GameObject("Tile_" + x + "_" + y);
                    obj.transform.parent = layerObject.transform;
                    obj.transform.localPosition = new Vector3(x, y, 0);

                    var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = GameTileset.masterTileset.tiles[tid];
                }
            }
        }
    }
}
