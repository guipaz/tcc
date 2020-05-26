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
            finishedExecution = false;
            Debug.Log(message);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                finishedExecution = true;
        }
    }
}
