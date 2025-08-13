using Match3.Scripts.Character;
using Match3.Subscripts;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Match3.Scripts
{
    public enum ECellType
    {
        Sword, Shield, Skull, Health, Fire, Ice, Cloak
    }

    public class Cell
    {
        private TileSquare square;
        private Vector3Int position;
        private ECellType cellType;
        private Tile tile;

        private float damage;
        private float time;

        public ECellType CellType => cellType;
        public Tile Tile => tile;
        public Vector3Int Position => position;

        public Cell(TileSquare square, Vector3Int position)
        {
            this.square = square;
            this.position = position;
            cellType = Match3Sub.GetRandom();
            tile = Match3Sub.GetTile(cellType);
            CellStats stats = Match3Sub.GetStats(cellType);
            damage = stats.Damage;
            time = stats.Time;
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

        public virtual void ApplyEffect(CharacterBase target, CharacterBase source)
        {
            
        }
    }

}

