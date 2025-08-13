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
        private static readonly string path_stats = "CellTileData/CellStats";
        private static TileCellData data;
        private static CellStatsSO cellStats;
        

        public static readonly Dictionary<EDirection, Vector3Int> Directs = new()
        {
            {EDirection.Up, Vector3Int.up},
            {EDirection.Down, Vector3Int.down},
            {EDirection.Left, Vector3Int.left},
            {EDirection.Right, Vector3Int.right},
        };

        public static ECellType GetRandom()
        {
            int random = Random.Range(0, 7);
            switch(random)
            {
                case 0:
                    return ECellType.Shield;
                case 1:
                    return ECellType.Fire;
                case 2:
                    return ECellType.Skull;
                case 3:
                    return ECellType.Health;
                case 4:
                    return ECellType.Ice;
                case 5:
                    return ECellType.Cloak;
                default: return ECellType.Sword;
            }
        }

        public static CellStats GetStats(ECellType type) => cellStats.GetStats(type);

        public static void LoadCellTileData()
        {
            data = Resources.Load<TileCellData>(path);
            cellStats = Resources.Load<CellStatsSO>(path_stats);
        }

        public static Tile GetTile(ECellType cellType)=> data.GetTile(cellType);
        
    }
}

