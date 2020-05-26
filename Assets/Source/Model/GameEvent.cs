namespace Assets.Source.Model
{
    public abstract class GameEvent
    {
        public bool startedExecution;
        public bool finishedExecution;

        public abstract string GetNameText();
        public abstract string GetDescriptionText();
        public abstract void Execute();
        public abstract void Update();
    }
}