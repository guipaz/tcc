using Assets.Source.Model;
using UnityEngine;

namespace Assets.Source
{
    public static class Global
    {
        public static bool cursorAdd = true;
        public static Game game;
        public static GameObject cursorObject;
        public static Layers currentLayer;
        public static GameMap currentMap;
        public static Editor_MasterController master;
        public static GameObject canvasObject;
    }
}
