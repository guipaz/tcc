namespace Assets.Source.Model
{
    public class GameMapTileLayer
    {
        public int[,] tids;

        public GameMapTileLayer(int width, int height)
        {
            tids = new int[width, height];
        }
    }
}