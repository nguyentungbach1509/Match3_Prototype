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


        public ECellType CellType => cellType;
        public Tile Tile => tile;
        public Vector3Int Position => position;
        

        public Cell(TileSquare square, Vector3Int position)
        {
            this.square = square;
            this.position = position;
            cellType = Match3Sub.GetRandom();
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

    }

}

