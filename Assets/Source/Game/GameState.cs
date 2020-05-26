using Assets.Source.Model;

namespace Assets.Source.Game
{
    public class GameState
    {
        public static GameState main = new GameState();

        public GameMap currentMap;

        // event execution
        public GameEntity currentExecutedEntity;
        public int currentExecutedEventIndex;

        public void ExecuteEntity(GameEntity interactionEntity)
        {
            currentExecutedEntity = interactionEntity;
            currentExecutedEventIndex = 0;

            foreach (var ev in currentExecutedEntity.events)
            {
                ev.startedExecution = false;
                ev.finishedExecution = false;
            }
        }
    }
}
