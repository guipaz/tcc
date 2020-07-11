namespace Assets.Source.Model
{
    public abstract class GameEvent : IPersistent
    {
        public bool startedExecution;
        public bool finishedExecution;

        public abstract string GetNameText();
        public abstract string GetDescriptionText();
        public abstract void Execute();
        public abstract void Update();
        public abstract PersistenceData GetData();
        public abstract void SetData(PersistenceData data);
        public abstract string GetEventType();
    }
}