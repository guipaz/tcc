using Assets.Source.Game;
using Assets.Source.Model;

namespace Assets.Source.Events
{
    public class ChangeMapEvent : GameEvent
    {
        public string mapName;
        public int x;
        public int y;

        public override string GetNameText()
        {
            return "Mudar mapa";
        }

        public override string GetDescriptionText()
        {
            return mapName + " - {" + x + ";" + y + "}";
        }

        public override void Execute()
        {
            GameState.main.ChangeMap(mapName, x, y);

            finishedExecution = true;
        }

        public override void Update()
        {
            //TODO transition
        }
    }
}
