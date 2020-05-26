using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Events
{
    public class MessageEvent : GameEvent
    {
        public string message;

        public override string GetNameText()
        {
            return "Mensagem";
        }

        public override string GetDescriptionText()
        {
            return message;
        }

        public override void Execute()
        {
            MessagePanel.main.SetMessage(message);
            MessagePanel.main.Toggle(true);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MessagePanel.main.Toggle(false);
                finishedExecution = true;
            }
        }
    }
}
