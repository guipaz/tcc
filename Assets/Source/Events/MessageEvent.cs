using Assets.Source.Model;

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
    }
}
