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
            var mapsData = data.Get<List<PersistenceData>>("maps");
            foreach (var mapData in mapsData)
            {
                var width = mapData.Get<int>("width");
                var height = mapData.Get<int>("height");

                var map = new GameMap(width, height);
                map.SetData(mapData);
                maps.Add(map);
            }

            var playerData = data.Get<PersistenceData>("player");
            player.SetData(playerData);
        }
    }
}