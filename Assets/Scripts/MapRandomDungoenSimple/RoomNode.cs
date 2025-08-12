using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MapRandomDungoen
{
    public enum RoomType
    {
        Start, Normal, Boss, Shop
    }
    public enum EDirection
    {
        Up, Down, Left, Right
    }

    public struct Direction
    {
        private EDirection direct;
        private Vector2Int directVector;

        public EDirection Direct => direct;
        public Vector2Int DirectVector => directVector;

        public Direction(EDirection direct, Vector2Int directVector)
        {
            this.direct = direct;
            this.directVector = directVector;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Direction)) return false;
            Direction direction = (Direction)obj;
            return this.direct == direction.Direct && this.directVector == direction.DirectVector;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + direct.GetHashCode();
                hash = hash * 23 + directVector.GetHashCode();
                return hash;
            }
        }
    }

    public class RoomNode
    {
        private Vector2Int position;
        private RoomType roomType;

        public Vector2Int Position => position;
        public Dictionary<Direction, RoomNode> Neighbors { get; set; }
        public RoomType Type => roomType;

        public RoomNode(Vector2Int position, RoomType type)
        {
            this.position = position;
            this.roomType = type;
        }

        public void SetType(RoomType type) => roomType = type;
    }

}

