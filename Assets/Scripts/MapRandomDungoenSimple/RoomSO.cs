using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.MapRandomDungoen
{
    [CreateAssetMenu(fileName = "RoomSO", menuName = "Data/RoomSO")]
    public class RoomSO : ScriptableObject
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] Tile[] tiles;

        public int Width => width;
        public int Height => height;

        public Tile GetTile(RoomType type)
        {
            switch (type)
            {
                case RoomType.Boss:
                    return tiles[^1];
                case RoomType.Start:
                    return tiles[0];
                case RoomType.Shop:
                    return tiles[1];
                default:
                    return tiles[2];
            }
        }
    }
}


