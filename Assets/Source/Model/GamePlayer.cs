using UnityEngine;

namespace Assets.Source.Model
{
    public class GamePlayer : IPersistent
    {
        public Sprite sprite;
        public string startingMap;
        public int startingX;
        public int startingY;
        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("sprite", sprite?.ToJSON());
            data.Set("startingMap", startingMap);
            data.Set("startingX", startingX);
            data.Set("startingY", startingY);

            return data;
        }

        public void SetData(PersistenceData data)
        {
            throw new System.NotImplementedException();
        }
    }
}