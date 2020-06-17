using Assets.Source.Game;
using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Events
{
    class MoveEntityEvent : GameEvent
    {
        public string id;
        public int x;
        public int y;
        public bool relative = false;
        public bool immediate = true;

        public override string GetNameText()
        {
            return "Mover entidade";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + x + ";" + y + "}";
        }

        public override void Execute()
        {
            var obj = GameMasterBehaviour.main.GetEntityObject(id);

            if (obj != null)
            {
                if (immediate)
                {
                    var currentLocation = obj.GetComponent<EntityBehaviour>().location;
                    if (relative)
                    {
                        currentLocation.x += x;
                        currentLocation.y -= y;
                    }
                    else
                    {
                        var newY = GameState.main.currentGameMap.height - y - 1;
                        currentLocation = new Vector2(x, newY);
                    }

                    obj.GetComponent<EntityBehaviour>().location = currentLocation;
                    obj.GetComponent<Transform>().localPosition = new Vector3(currentLocation.x, currentLocation.y,
                        GameMasterBehaviour.main.player.transform.localPosition.z);

                    var player = obj.GetComponent<PlayerBehaviour>();
                    if (player != null)
                        player.CenterCamera();
                }
                else
                {
                    //TODO pathfinding
                }
            }

            finishedExecution = true;
        }

        public override void Update()
        {
        }

        public override PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("event", GetType().FullName);
            data.Set("id", id);
            data.Set("x", x);
            data.Set("y", y);
            data.Set("immediate", immediate);
            data.Set("relative", relative);

            return data;
        }

        public override void SetData(PersistenceData data)
        {
            id = data.Get("id", id);
            x = data.Get("x", x);
            y = data.Get("y", y);
            immediate = data.Get("immediate", immediate);
            relative = data.Get("relative", relative);
        }
    }
}
