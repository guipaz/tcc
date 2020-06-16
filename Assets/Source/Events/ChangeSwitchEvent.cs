using System;
using Assets.Source.Game;
using Assets.Source.Model;

namespace Assets.Source.Events
{
    public class ChangeSwitchEvent : GameEvent
    {
        public string id;
        public bool flag;

        public override string GetNameText()
        {
            return "Ativar/desativar interruptor";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + flag + "}";
        }

        public override void Execute()
        {
            GameState.main.switches[id] = flag;

            finishedExecution = true;
        }

        public override void Update()
        {
        }
    }
}
