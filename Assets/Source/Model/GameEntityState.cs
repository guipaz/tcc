using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Model
{
    public class GameEntityState
    {
        public const string DEFAULT_STATE_NAME = "Padrao";

        public bool variableCheck;
        public string variableCheckName;
        public int variableCheckValue;

        public bool switchCheck;
        public string switchCheckName;
        public bool switchCheckValue;

        public string name;
        public EntityExecution execution;
        public Sprite image;
        public List<GameEvent> events;
        public bool passable = false;

        public GameEntityState()
        {
            events = new List<GameEvent>();
        }
    }
}