using System.Linq;

namespace Assets.Source.Model
{
    public class GameMapTileLayer : IPersistent
    {
        public int[,] tids;

        int width;
        int height;

        public GameMapTileLayer(int width, int height)
        {
            this.width = width;
            this.height = height;
            tids = new int[width, height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    tids[x, y] = -1;
        }

        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("width", width);
            data.Set("height", height);

            var i = 0;
            var linearTids = new int[width * height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    linearTids[i++] = tids[x, y];
            
            data.Set("tids", string.Join(",", linearTids.Select(x => x.ToString()).ToArray()));

            return data;
        }

        public void SetData(PersistenceData data)
        {
            throw new System.NotImplementedException();
        }
    }
}