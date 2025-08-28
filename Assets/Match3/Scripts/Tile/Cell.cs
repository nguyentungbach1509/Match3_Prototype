using Match3.Scripts.Character;
using Match3.Scripts.Data;
using Match3.Subscripts;
using Match3.SubScripts;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Match3.Scripts
{
   
    public class Cell
    {
        private TileSquare square;
        private Vector3Int position;
        private ECellType cellType;
        private Tile tile;
        private ECellType subType;

        public ECellType CellType => cellType;
        public Tile Tile => tile;
        public Vector3Int Position => position;
        public ECellType SubType => subType;

        public Cell(TileSquare square, Vector3Int position, ECellType type=ECellType.None)
        {
            this.square = square;
            this.position = position;
            cellType = type == ECellType.None ? Match3Sub.GetRandom() : type;
            subType = cellType;
            tile = Match3Sub.GetTile(cellType);
        }

        public virtual bool Compare(Cell other)
        {
            return this.cellType == other.CellType;
        }

        public virtual void Assign(TileSquare square)
        {
            this.square = square;
            this.position = square.Position;
        }

        public void SetCellType(ECellType cellType) => this.cellType = cellType;
    }

}

