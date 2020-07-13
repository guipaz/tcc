using System.Collections.Generic;
using Assets.Source;
using Assets.Source.Model;
using UnityEngine;

public static class Global
{
    public static bool cursorAdd = true;
    public static Game game;
    public static GameObject cursorObject;
    public static Layers currentLayer;
    public static GameMap currentMap;
    public static Editor_MasterController master;
    public static GameObject canvasObject;
    public static Dictionary<string, GameTileset> tilesets;
    public static string loadGame;
    public static Sprite[] TilesetSprites;
    public static bool playGame;
}