using System;
using Assets.Source;
using Assets.Source.Game;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterBehaviour : MonoBehaviour
{
    public static GameMasterBehaviour main;

    public PlayerBehaviour player;
    public GameObject entityPrefab;

    void Start()
    {
        main = this;

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
            if (GameState.main.currentExecutedEntity.events.Count > GameState.main.currentExecutedEventIndex)
            {
                var currentEvent =
                    GameState.main.currentExecutedEntity.events[GameState.main.currentExecutedEventIndex];

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

        var layerObject = new GameObject(Layers.Entities.ToString());
        layerObject.transform.parent = mapObject.transform;
        foreach (var entity in map.entityLayer.entities)
        {
            var entityObject = Instantiate(entityPrefab, layerObject.transform);
            entityObject.name = string.IsNullOrEmpty(entity.name) ? "Entity" : entity.name;
            entityObject.transform.localPosition = new Vector3(entity.location.x, entity.location.y, 0);
            entityObject.GetComponent<SpriteRenderer>().sprite = entity.image;
            entityObject.GetComponent<EntityBehaviour>().gameEntity = entity;
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
                    spriteRenderer.sprite = map.tileset.tiles[tid];
                }
            }
        }
    }
}
