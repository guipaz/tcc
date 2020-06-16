using System.Collections.Generic;

namespace Assets.Source.Model
{
    public class GameMap : IPersistent
    {
        public string name;
        public readonly int width;
        public readonly int height;

        public readonly GameMapTileLayer terrainLayer;
        public readonly GameMapTileLayer constructionLayer;
        public readonly GameMapTileLayer aboveLayer;
        public readonly List<GameEntity> entities;

        public GameMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            terrainLayer = new GameMapTileLayer(width, height);
            constructionLayer = new GameMapTileLayer(width, height);
            aboveLayer = new GameMapTileLayer(width, height);

            entities = new List<GameEntity>();
        }

        public bool IsInside(int x, int y)
        {
            return x >= 0 && y >= 0 && x < width && y < height;
        }

        public PersistenceData GetData()
        {
            var data = new PersistenceData();

            data.Set("name", name);
            data.Set("width", width);
            data.Set("height", height);

            data.Set("terrainLayer", terrainLayer.GetData());
            data.Set("constructionLayer", constructionLayer.GetData());
            data.Set("aboveLayer", aboveLayer.GetData());

            var entitiesData = new List<PersistenceData>();
            foreach (var entity in entities)
                entitiesData.Add(entity.GetData());

            data.Set("entities", entitiesData);

            return data;
        }

        public void SetData(PersistenceData data)
        {
            name = data.Get("name", name);

            terrainLayer.SetData(data.Get<PersistenceData>("terrainLayer"));
            constructionLayer.SetData(data.Get<PersistenceData>("constructionLayer"));
            aboveLayer.SetData(data.Get<PersistenceData>("aboveLayer"));

            var entitiesData = data.Get<List<PersistenceData>>("entities");
            foreach (var entityData in entitiesData)
            {
                var entity = new GameEntity();
                entity.SetData(entityData);
                entities.Add(entity);
            }
        }
    }
}
