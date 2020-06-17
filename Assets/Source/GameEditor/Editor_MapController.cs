using Assets.Source;
using Assets.Source.Model;
using UnityEngine;

public class Editor_MapController : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject entityPrefab;

    public void Commit()
    {
        //TODO put changes inside map

        var terrainLayer = GameObject.Find(Layers.Terrain.ToString());
        var constructionLayer = GameObject.Find(Layers.Construction.ToString());
        var aboveLayer = GameObject.Find(Layers.Above.ToString());

        AddToLayer(terrainLayer, Global.currentMap.terrainLayer);
        AddToLayer(constructionLayer, Global.currentMap.constructionLayer);
        AddToLayer(aboveLayer, Global.currentMap.aboveLayer);
    }

    void AddToLayer(GameObject layerObject, GameMapTileLayer tileLayer)
    {
        foreach (Transform child in layerObject.transform)
        {
            var editorTile = child.GetComponent<EditorTile>();
            tileLayer.tids[editorTile.x, editorTile.y] = editorTile.tid;
        }
    }

    public void GenerateCurrentMap()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        var width = Global.currentMap.width;
        var height = Global.currentMap.height;

        GenerateLayer(Layers.Terrain.ToString(), width, height, 3, Global.currentMap.terrainLayer);
        GenerateLayer(Layers.Construction.ToString(), width, height, 2, Global.currentMap.constructionLayer);
        GenerateLayer(Layers.Above.ToString(), width, height, 1, Global.currentMap.aboveLayer);

        // entities
        var layer = new GameObject(Layers.Entities.ToString());
        layer.transform.parent = transform;
        layer.transform.localPosition = new Vector3(0, 0, 0);

        foreach (var gameEntity in Global.currentMap.entities)
        {
            var entity = Instantiate(entityPrefab, layer.transform);
            entity.transform.localPosition = new Vector3((int)(gameEntity.location.x + 0.5f), (int)(gameEntity.location.y + 0.5f), 0);

            var editorEntity = entity.GetComponent<EditorEntity>();
            editorEntity.gameEntity = gameEntity;
            editorEntity.GetComponent<SpriteRenderer>().sprite = editorEntity?.gameEntity?.states[GameEntityState.DEFAULT_STATE_NAME].image ?? Resources.Load<Sprite>("icon_entity");
        }

        // camera position
        Camera.main.transform.position = new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, Camera.main.transform.position.z);
    }

    void GenerateLayer(string name, int width, int height, int depth, GameMapTileLayer tileLayer)
    {
        var layer = new GameObject(name);
        layer.transform.parent = transform;
        layer.transform.localPosition = new Vector3(0, 0, depth);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tid = tileLayer.tids[x, y];

                var obj = Instantiate(tilePrefab, layer.transform, true);
                obj.transform.localPosition = new Vector3(x, y, 0);
                obj.GetComponent<SpriteRenderer>().sprite = tid != -1 ? GameTileset.masterTileset.tiles[tid] : null;

                var editorTile = obj.GetComponent<EditorTile>();
                editorTile.x = x;
                editorTile.y = y;
                editorTile.tid = tid;
            }
        }
    }

    public Material lineMaterial;
    public void OnRenderObject()
    {
        lineMaterial.SetPass(0);
        
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix * Matrix4x4.Translate(new Vector3(-0.5f, -0.5f)));

        var width = Global.currentMap.width;
        var height = Global.currentMap.height;

        // grid
        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 1, 1, 0.5f));

        for (var y = 0; y < height; y++)
        {
            GL.Vertex3(0, y, 0);
            GL.Vertex3(width, y, 0);
        }

        for (var x = 0; x < width; x++)
        {
            GL.Vertex3(x, 0, 0);
            GL.Vertex3(x, height, 0);
        }

        GL.End();

        // borders
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);

        var lineThickness = 0.1f;
        
        // left
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, height, 0);
        GL.Vertex3(lineThickness, height, 0);
        GL.Vertex3(lineThickness, 0, 0);

        // up
        GL.Vertex3(0, height, 0);
        GL.Vertex3(width, height, 0);
        GL.Vertex3(width, height - lineThickness, 0);
        GL.Vertex3(0, height - lineThickness, 0);

        // right
        GL.Vertex3(width - lineThickness, height, 0);
        GL.Vertex3(width, height, 0);
        GL.Vertex3(width, 0, 0);
        GL.Vertex3(width - lineThickness, 0, 0);

        // down
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, lineThickness, 0);
        GL.Vertex3(width, lineThickness, 0);
        GL.Vertex3(width, 0, 0);

        GL.End();
        GL.PopMatrix();
    }
}
