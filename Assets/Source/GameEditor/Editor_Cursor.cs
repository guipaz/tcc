using Assets.Source;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class Editor_Cursor : MonoBehaviour
{
    public GameObject entityPrefab;

    public int currentTid = -1;

    void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = new Vector3((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), 1);

        if (Global.currentLayer == Layers.Entities)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject entity = null;

                // tries to fetch an existing entity
                var hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        var parentName = hit.collider.gameObject.transform.parent.name;
                        if (parentName == Global.currentLayer.ToString())
                        {
                            entity = hit.collider.gameObject;
                            break;
                        }
                    }
                }

                // if none exists, create one
                if (entity == null)
                {
                    entity = Instantiate(entityPrefab, GameObject.Find(Layers.Entities.ToString()).transform);
                    entity.transform.localPosition = new Vector3((int)(pos.x + 0.5f), (int)(pos.y + 0.5f), 0);

                    var gameEntity = new GameEntity
                    {
                        location = new Vector2(entity.transform.localPosition.x, entity.transform.localPosition.y)
                    };
                    Global.currentMap.entities.Add(gameEntity);
                    entity.GetComponent<EditorEntity>().gameEntity = gameEntity;
                }
                
                // opens the entity panel
                var panel = Global.master.OpenPanel("entity");
                panel.GetComponent<Panel_Entity>().SetData(entity.GetComponent<EditorEntity>());
            }
        }
        else
        {
            // hiding when out of bounds
            if (pos.x < -0.5f || pos.y < -0.5f || pos.x >= Global.currentMap.width - 0.5f || pos.y >= Global.currentMap.height - 0.5f)
            {
                GetComponent<SpriteRenderer>().color = Color.white * 0f;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

            // draw tiles if the canvas is not on the way
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hits.Length > 0)
                {
                    foreach (var hit in hits)
                    {
                        var parentName = hit.collider.gameObject.transform.parent.name;
                        if (parentName == Global.currentLayer.ToString())
                        {
                            hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = Global.cursorAdd ? GetComponent<SpriteRenderer>().sprite : null;
                            hit.collider.gameObject.GetComponent<EditorTile>().tid = currentTid;

                            break;
                        }
                    }
                }
            }
        }
    }

    public void SetTile(int tid)
    {
        var tile = tid >= 0 ? GameTileset.masterTileset.tiles[tid] : null;
        Global.cursorObject.GetComponent<SpriteRenderer>().sprite = tile;
        currentTid = tid;
    }
}
