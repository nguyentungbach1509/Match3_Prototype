using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts
{

    public class TileSquare
    {
        private Vector3Int position;
        private Cell cell;

        #region Neighbours
        public TileSquare Top { get; set; }
        public TileSquare Down { get; set; }
        public TileSquare Right { get; set; }
        public TileSquare Left { get; set; }
        #endregion

        public Cell Cell => cell;
        public Vector3Int Position => position;

        public TileSquare(Vector3Int position)
        {
            cell = new Cell(this, position);
            this.position = position; 
        }

        public void SetAndAssignCell(Cell other)
        {
            this.cell = other;
            other.Assign(this);
        }

        public void SetCell(Cell other) { this.cell = other; }

        public bool IsNeighbour(TileSquare other)
        {
            bool horizontal = Mathf.Abs(other.Position.x - position.x) == 1
                && other.Position.y == position.y;
            bool vertical = Mathf.Abs(other.Position.y - position.y) == 1 &&
                other.Position.x == position.x;
            return horizontal || vertical;
        }

        public void SwapCell(TileSquare other)
        {
            if (IsNeighbour(other))
            {
                Cell tempCell = other.Cell;
                other.SetAndAssignCell(this.cell);
                this.SetAndAssignCell(tempCell);
            }
        }
    }
}

