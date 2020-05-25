using System.Collections.Generic;

namespace Assets.Source.Model
{
    public class GameMapEntityLayer
    {
        public readonly List<GameEntity> entities;

        public GameMapEntityLayer()
        {
            entities = new List<GameEntity>();
        }
    }
}