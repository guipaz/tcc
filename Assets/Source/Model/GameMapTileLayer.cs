namespace Assets.Source.Model
{
    public class GameMapTileLayer
    {
        public int[,] tids;

        public GameMapTileLayer(int width, int height)
        {
            tids = new int[width, height];
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    tids[x, y] = -1;
        }
    }
}