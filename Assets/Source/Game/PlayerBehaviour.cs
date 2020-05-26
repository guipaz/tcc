using Assets.Source.Game;
using Assets.Source.Model;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector2 mov;

    public Vector2 interactionVector;

    void Update()
    {
        // entity being executed
        if (GameState.main.currentExecutedEntity != null)
        {
            return;
        }

        mov.x = 0;
        mov.y = 0;

        GameEntity interactionEntity = null;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            mov.x = 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            mov.x = -1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            mov.y = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            mov.y = -1;

        if (mov != Vector2.zero)
        {
            interactionVector = mov;

            var finalPos = new Vector2(transform.localPosition.x + mov.x, transform.localPosition.y + mov.y);

            // collision
            var blockedByEntity = false;
            foreach (var entity in Global.currentMap.entityLayer.entities)
            {
                if (entity.location == finalPos)
                {
                    if (entity.passable && entity.execution == EntityExecution.Contact)
                    {
                        interactionEntity = entity;
                    }
                    else
                    {
                        blockedByEntity = true;
                    }
                    
                    break;
                }
            }

            if (Global.currentMap.IsInside((int)finalPos.x, (int)finalPos.y) && !blockedByEntity && Global.currentMap.constructionLayer.tids[(int)finalPos.x, (int)finalPos.y] == -1)
            {
                transform.localPosition = new Vector3(finalPos.x, finalPos.y, transform.localPosition.z);
                CenterCamera();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (var entity in Global.currentMap.entityLayer.entities)
            {
                var interactionLocation = new Vector2(transform.localPosition.x + interactionVector.x,
                    transform.localPosition.y + interactionVector.y);

                if (entity.location == interactionLocation && entity.execution == EntityExecution.Interaction)
                {
                    interactionEntity = entity;
                    break;
                }
            }
        }

        if (interactionEntity != null)
        {
            GameState.main.ExecuteEntity(interactionEntity);
        }
    }

    public void CenterCamera()
    {
        Camera.main.transform.localPosition = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.localPosition.z);
    }
}
