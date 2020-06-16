using System.Collections.Generic;

namespace Assets.Source.Model
{
    public class Game : IPersistent
    {
        public List<GameMap> maps;
        //public List<GameItem> items;
        public GamePlayer player;

        public Game()
        {
            maps = new List<GameMap>();
            //items = new List<GameItem>();
            player = new GamePlayer();
        }

        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            var mapsData = new List<PersistenceData>();
            foreach (var map in maps)
                mapsData.Add(map.GetData());

            data.Set("maps", mapsData);
            data.Set("player", player.GetData());

            return data;
        }

        public void SetData(PersistenceData data)
        {
            throw new System.NotImplementedException();
        }
    }
}