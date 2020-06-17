using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace Assets.Source.Model
{
    public enum EntityExecution
    {
        Interaction = 0,
        Contact,
        Automatic
    }

    public class GameEntity : IPersistent
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

        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("name", name);
            data.Set("location", location.ToJSON());

            var statesData = new PersistenceData();
            foreach (var key in states.Keys)
            {
                var state = states[key];
                statesData.Set(key, state.GetData());
            }
            data.Set("states", statesData);

            return data;
        }

        public void SetData(PersistenceData data)
        {
            name = data.Get("name", name);
            location = data.Get("location", location);

            var statesData = data.Get<PersistenceData>("states");
            foreach (var key in statesData.InternalData.Keys)
            {
                var stateData = statesData.InternalData[key];
                var objValue = stateData.ObjectValue;

                var state = new GameEntityState();
                state.SetData(new PersistenceData(objValue));
                states[state.name] = state;
            }
        }
    }
}