using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Model
{
    public enum EntityExecution
    {
        Interaction = 0,
        Contact,
        Automatic
    }

    public class GameEntity
    {
        public string name;
        public EntityExecution execution;
        public Sprite image;
        public List<GameEvent> events;

        public GameEntity()
        {
            events = new List<GameEvent>();
        }
    }
}