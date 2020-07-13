using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Model;

namespace Assets.Source.Events
{
    public class EndGameEvent : GameEvent
    {
        public override string GetNameText()
        {
            return "Finalizar jogo";
        }

        public override string GetDescriptionText()
        {
            return "";
        }

        public override void Execute()
        {
            GameMasterBehaviour.main.EndGame();
        }

        public override void Update()
        {
        }

        public override PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("event", GetType().FullName);

            return data;
        }

        public override void SetData(PersistenceData data)
        {
        }

        public override string GetEventType()
        {
            return "endGame";
        }
    }
}
