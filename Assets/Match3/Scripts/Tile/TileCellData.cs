using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3.Scripts
{
    [CreateAssetMenu(fileName = "TileCellData", menuName = "Data/Cell/TileData")]
    public class TileCellData : ScriptableObject
    {
        [SerializeField] List<TileCell> tileCells;
        private Dictionary<ECellType, Tile> tileDict = new();

        public Tile GetTile(ECellType key)
        {
            if(tileDict.TryGetValue(key, out Tile tile)) return tile;
            Tile cellTile = tileCells.Find(x => x.Type == key).Tile;
            tileDict[key] = cellTile;
            return cellTile;
        }
    }

    [Serializable]
    public class TileCell
    {
        public ECellType Type;
        public Tile Tile;
    }
}

