using Assets.Source;
using Assets.Source.Model;
using UnityEngine;

public class GameMasterBehaviour : MonoBehaviour
{
    public PlayerBehaviour player;

    void Start()
    {
        //TODO load game
        //TODO instantiate first map
        Debug.Log(Global.currentMap);

        InstantiateMap(Global.currentMap);

        player.transform.localPosition = new Vector3(Global.currentMap.width / 2, Global.currentMap.height / 2, player.transform.localPosition.z);

        player.CenterCamera();
    }

    void InstantiateMap(GameMap map)
    {
        var mapObject = GameObject.Find("Map");
        foreach (Transform child in mapObject.transform)
            Destroy(child);

        InstantiateLayer(mapObject, map, Layers.Terrain, map.terrainLayer, 2);
        InstantiateLayer(mapObject, map, Layers.Construction, map.constructionLayer, 1);
        InstantiateLayer(mapObject, map, Layers.Above, map.aboveLayer, -1);

        //TODO entities
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
