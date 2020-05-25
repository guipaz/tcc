using System.Collections.Generic;

namespace Assets.Source.Model
{
    public class Game
    {
        public List<GameMap> maps;
        public List<GameItem> items;
        public GamePlayer player;

        public Game()
        {
            maps = new List<GameMap>();
            items = new List<GameItem>();
            player = new GamePlayer();
        }
    }
}