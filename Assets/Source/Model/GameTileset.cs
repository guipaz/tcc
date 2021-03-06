﻿using UnityEngine;

namespace Assets.Source.Model
{
    public class GameTileset
    {
        public const string MASTER_TILESET_ID = "Master_Tileset";
        public static GameTileset masterTileset = new GameTileset(MASTER_TILESET_ID, Resources.Load<Sprite>("DawnLike/Master_Tileset"));

        public string name;
        public Sprite originalImage;
        public Sprite[] tiles;

        public GameTileset(string name, Sprite originalImage, int tileSize = 16)
        {
            this.name = name;
            this.originalImage = originalImage;

            var tilesX = originalImage.texture.width / tileSize;
            var tilesY = originalImage.texture.height / tileSize;
            tiles = new Sprite[tilesX * tilesY];

            var pivot = new Vector2(0.5f, 0.5f);
            for (var y = 0; y < tilesY; y++)
            {
                for (var x = 0; x < tilesX; x++)
                {
                    tiles[(tilesY - y - 1) * tilesX + x] = Sprite.Create(originalImage.texture, new Rect(x * tileSize, y * tileSize, tileSize, tileSize), pivot, tileSize);
                }
            }
        }
    }
}