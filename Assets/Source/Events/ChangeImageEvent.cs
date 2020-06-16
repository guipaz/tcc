using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source.Events
{
    public class ChangeImageEvent : GameEvent
    {
        public string id;
        public Sprite sprite;

        public override string GetNameText()
        {
            return "Mudar imagem";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + sprite.name + "}";
        }

        public override void Execute()
        {
            var obj = GameMasterBehaviour.main.GetEntityObject(id);
            
            if (obj != null)
                obj.GetComponent<SpriteRenderer>().sprite = sprite;

            finishedExecution = true;
        }

        public override void Update()
        {
            //TODO transition
        }
    }
}
