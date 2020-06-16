using System.Collections.Generic;
using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Game
{
    public class GameState
    {
        public static GameState main = new GameState();

        public GameMap currentGameMap;
        public List<EntityBehaviour> currentEntityBehaviours = new List<EntityBehaviour>();
        public Dictionary<string, bool> switches = new Dictionary<string, bool>();
        public Dictionary<string, int> variables = new Dictionary<string, int>();

        // event execution
        public EntityBehaviour currentExecutedEntity;
        public int currentExecutedEventIndex;

        public void ExecuteEntity(EntityBehaviour entity)
        {
            currentExecutedEntity = entity;
            currentExecutedEventIndex = 0;

            foreach (var ev in currentExecutedEntity.currentState.events)
            {
                ev.startedExecution = false;
                ev.finishedExecution = false;
            }
        }

        public void ChangeMap(string mapName, int x, int y)
        {
            GameMap curr = null;
            foreach (var map in Global.game.maps)
            {
                if (map.name == mapName)
                {
                    curr = map;
                    break;
                }
            }

            if (curr == null)
                return;

            currentGameMap = curr;
            currentEntityBehaviours.Clear();

            GameMasterBehaviour.main.InstantiateMap(currentGameMap);
            GameMasterBehaviour.main.player.transform.localPosition = new Vector3(x, currentGameMap.height - y - 1, GameMasterBehaviour.main.player.transform.localPosition.z);
            GameMasterBehaviour.main.player.CenterCamera();

            foreach (var e in currentEntityBehaviours)
            {
                if (e.currentState.execution == EntityExecution.Automatic)
                {
                    ExecuteEntity(e);
                    break;
                }
            }
        }

        public void Clear()
        {
            switches.Clear();
            variables.Clear();
            currentEntityBehaviours.Clear();

            currentGameMap = null;
            currentExecutedEntity = null;
            currentExecutedEventIndex = 0;
        }
    }
}
