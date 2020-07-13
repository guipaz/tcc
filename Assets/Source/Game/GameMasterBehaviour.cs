using Assets.Source;
using Assets.Source.Game;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameState = Assets.Source.Game.GameState;

public class GameMasterBehaviour : MonoBehaviour
{
    public static GameMasterBehaviour main;

    public PlayerBehaviour player;
    public GameObject entityPrefab;
    
    public GameObject gameNamePanel;
    public Text gameNameText;

    public bool IsPlayerLocked => currentTransition != null;

    Transition startingTransition;
    Transition endingTransition;

    Transition currentTransition;

    GameObject entitiesLayer;

    void Start()
    {
        main = this;

        GameState.main.Clear();

        MessagePanel.main.Toggle(false);

        var playerEntity = new GameEntity();
        player.GetComponent<EntityBehaviour>().gameEntity = playerEntity;
        player.GetComponent<SpriteRenderer>().sprite = Global.game.player.sprite;

        var startingMap = Global.game.player.startingMap;
        var startingX = Global.game.player.startingX;
        var startingY = Global.game.player.startingY;

        GameState.main.ChangeMap(startingMap, startingX, startingY);

        startingTransition = new Transition
        {
            duration = 3,
            OnStart = () =>
            {
                gameNamePanel.SetActive(true);
                gameNamePanel.GetComponent<CanvasGroup>().alpha = 1;
                gameNameText.text = Global.game.name;
            },
            OnUpdate = timeLeft =>
            {
                if (timeLeft <= 1)
                {
                    gameNamePanel.GetComponent<CanvasGroup>().alpha = timeLeft;
                }
            },
            OnFinish = () =>
            {
                gameNamePanel.SetActive(false);
                currentTransition = null;
            }
        };

        endingTransition = new Transition
        {
            duration = 5,
            OnStart = () =>
            {
                gameNamePanel.SetActive(true);
                gameNamePanel.GetComponent<CanvasGroup>().alpha = 0;
                gameNameText.text = "Fim";
            },
            OnUpdate = timeLeft =>
            {
                if (timeLeft >= 4)
                {
                    gameNamePanel.GetComponent<CanvasGroup>().alpha = 5 - timeLeft;
                }
                else
                {
                    gameNamePanel.GetComponent<CanvasGroup>().alpha = 1;
                }
            },
            OnFinish = () =>
            {
                currentTransition = null;
                ExitGame();
            }
        };

        PlayTransition(startingTransition);
    }

    void ExitGame()
    {
        if (Global.playGame)
            SceneManager.LoadScene("MenuScene");
        else
            SceneManager.LoadScene("EditorScene");
    }

    void PlayTransition(Transition t)
    {
        currentTransition = t;
        t?.Play();
    }

    public GameObject GetEntityObject(string id)
    {
        if (id == PlayerBehaviour.PLAYER_ID)
            return player.gameObject;

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
        if (currentTransition?.Update() ?? false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
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

        foreach (var entity in map.entities)
        {
            var entityObject = Instantiate(entityPrefab, entitiesLayer.transform);
            entityObject.name = string.IsNullOrEmpty(entity.name) ? "Entity" : entity.name;
            
            var entityBehaviour = entityObject.GetComponent<EntityBehaviour>();

            EntityBehaviour currentBehaviour = null;
            foreach (var behaviour in GameState.main.currentEntityBehaviours)
            {
                if (behaviour.gameEntity == entity)
                {
                    currentBehaviour = behaviour;
                    break;
                }
            }

            entityBehaviour.gameEntity = currentBehaviour?.gameEntity ?? entity;
            entityBehaviour.location = currentBehaviour?.location ?? entity.location;

            entityObject.transform.localPosition = new Vector3(entityBehaviour.location.x, entityBehaviour.location.y, 0);

            ProcessState(entityBehaviour);

            entityObject.GetComponent<SpriteRenderer>().sprite = entityBehaviour.currentState.image;

            if (currentBehaviour != null)
                GameState.main.currentEntityBehaviours.Remove(currentBehaviour); 

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

            // either way, will continue iterating because the last one is the one that has most priority
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
                    spriteRenderer.sortingOrder = layerType == Layers.Above ? 1 : 0;
                }
            }
        }
    }

    public void EndGame()
    {
        PlayTransition(endingTransition);
    }
}
