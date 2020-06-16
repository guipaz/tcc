using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Model
{
    public class GameEntityState : IPersistent
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

        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("variableCheck", variableCheck);
            data.Set("variableCheckName", variableCheckName);
            data.Set("variableCheckValue", variableCheckValue);

            data.Set("switchCheck", switchCheck);
            data.Set("switchCheckName", switchCheckName);
            data.Set("switchCheckValue", switchCheckValue);

            data.Set("name", name);
            data.Set("execution", (int)execution);
            data.Set("image", image?.ToJSON());
            data.Set("passable", passable);

            var eventsData = new List<PersistenceData>();
            foreach (var ev in events)
                eventsData.Add(ev.GetData());

            data.Set("events", eventsData);

            return data;
        }

        public void SetData(PersistenceData data)
        {
            name = data.Get("name", name);

            variableCheck = data.Get("variableCheck", variableCheck);
            variableCheckName = data.Get("variableCheckName", variableCheckName);
            variableCheckValue = data.Get("variableCheckValue", variableCheckValue);

            switchCheck = data.Get("switchCheck", switchCheck);
            switchCheckName = data.Get("switchCheckName", switchCheckName);
            switchCheckValue = data.Get("switchCheckValue", switchCheckValue);

            execution = (EntityExecution)data.Get<int>("execution");
            image = data.Get("image", image);
            passable = data.Get("passable", passable);

            var eventsData = data.Get<List<PersistenceData>>("events");
            foreach (var eventData in eventsData)
            {
                var eventType = eventData.Get<string>("event");
                var type = Type.GetType(eventType);
                var ev = (GameEvent) Activator.CreateInstance(type);
                ev.SetData(eventData);
                events.Add(ev);
            }
        }
    }
}