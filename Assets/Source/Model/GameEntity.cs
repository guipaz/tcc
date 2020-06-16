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
        public Vector2 location;
        public Dictionary<string, GameEntityState> states;

        public GameEntity()
        {
            states = new Dictionary<string, GameEntityState>
            {
                [GameEntityState.DEFAULT_STATE_NAME] =
                    new GameEntityState {name = GameEntityState.DEFAULT_STATE_NAME}
            };
        }
    }
}