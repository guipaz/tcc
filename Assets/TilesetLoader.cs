using System.Collections.Generic;
using Assets.Source.Model;
using UnityEngine;

public class TilesetLoader : MonoBehaviour
{
    public Sprite[] TilesetSprites;

    void Start()
    {
        // saves this sprites for the ExtPersistence
        Global.TilesetSprites = TilesetSprites;

        // slices and loads the tilesets based on the sprites
        Global.tilesets = new Dictionary<string, GameTileset>();
        foreach (var sprite in TilesetSprites)
        {
            Global.tilesets[sprite.name] = new GameTileset(sprite.name, sprite);
        }
    }
}
