using Match3.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3.Subscripts
{
    public enum EDirection
    {
        Up, Down, Left, Right
    }

    public static class Match3Sub
    {
        private static readonly string path = "CellTileData/Data";
        private static TileCellData data;
        public static Dictionary<ECellType, Color> Colors = new() {
            { ECellType.Red, Color.red },
            { ECellType.Green, Color.green },
            { ECellType.Blue, Color.blue },
            { ECellType.Yellow, Color.yellow },
            {ECellType.Cyan, Color.cyan },
        };

        public static readonly Dictionary<EDirection, Vector3Int> Directs = new()
        {
            {EDirection.Up, Vector3Int.up},
            {EDirection.Down, Vector3Int.down},
            {EDirection.Left, Vector3Int.left},
            {EDirection.Right, Vector3Int.right},
        };

        public static ECellType GetRandom()
        {
            int random = Random.Range(0, 5);
            switch(random)
            {
                case 0:
                    return ECellType.Blue;
                case 1:
                    return ECellType.Green;
                case 2:
                    return ECellType.Yellow;
                case 3:
                    return ECellType.Cyan;
                case 4:
                    return ECellType.Black;
                case 5:
                    return ECellType.Gray;
                case 6:
                    return ECellType.Magenta;
                default: return ECellType.Red;
            }
        }

        public static void LoadCellTileData()
        {
            data = Resources.Load<TileCellData>(path);
        }


        public static Tile GetTile(ECellType cellType)=> data.GetTile(cellType);
        
    }
}

