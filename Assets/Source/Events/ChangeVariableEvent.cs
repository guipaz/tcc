using Assets.Source.Game;
using Assets.Source.Model;

namespace Assets.Source.Events
{
    public class ChangeVariableEvent : GameEvent
    {
        public string id;
        public int operation; // 0 - Set / 1 - Sum / 2 - Subtract
        public int value;

        public override string GetNameText()
        {
            return "Mudar valor de variável";
        }

        public override string GetDescriptionText()
        {
            return id + " - {" + GetOperationName() + ": " + value + "}";
        }

        string GetOperationName()
        {
            switch (operation)
            {
                case 1:
                    return "Soma";
                case 2:
                    return "Subtrair";
            }

            return "Definir";
        }

        public override void Execute()
        {
            if (!GameState.main.variables.ContainsKey(id))
                GameState.main.variables[id] = 0;

            switch (operation)
            {
                case 1:
                    GameState.main.variables[id] += value;
                    break;
                case 2:
                    GameState.main.variables[id] -= value;
                    break;
                default:
                    GameState.main.variables[id] = value;
                    break;
            }

            // revalidates entities
            GameMasterBehaviour.main.InstantiateEntities(GameState.main.currentGameMap);

            finishedExecution = true;
        }

        public override void Update()
        {
        }

        public override PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("event", GetType().FullName);
            data.Set("id", id);
            data.Set("operation", operation);
            data.Set("value", value);

            return data;
        }

        public override void SetData(PersistenceData data)
        {
            id = data.Get("id", id);
            operation = data.Get("operation", operation);
            value = data.Get("value", value);
        }
    }
}
