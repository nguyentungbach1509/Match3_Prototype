using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.MapRandomDungoen
{
    public class Room : MonoBehaviour
    {
        [SerializeField] RoomSO data;

        private RoomNode roomNode;
        private Tilemap tileMap;

        private int width;
        private int height;
        private Vector3Int position;

        public void Init(Tilemap tiles, RoomNode node)
        {
            width = data.Width;
            height = data.Height;
            position = new Vector3Int(node.Position.x * width * 2, node.Position.y * height * 2, 0);
            transform.position = new Vector3(node.Position.x, node.Position.y, 0);
            roomNode = node;
            tileMap = tiles;
            RoomBuild(tiles);
        }

        private void RoomBuild(Tilemap tiles)
        {
            //Vector3Int pos = new Vector3Int(roomNode.Position.x, roomNode.Position.y, 0);
            //tiles.SetTile(pos, data.GetTile(roomNode.Type));
            int y = -(int)(data.Height * .5f) + position.y;
            for (int row = 0; row < data.Height; row++)
            {
                int x = -(int)(data.Width * .5f) + position.x;
                for (int col = 0; col < data.Width; col++)
                {
                    Vector3Int pos = new Vector3Int(x, y);
                    tiles.SetTile(pos, data.GetTile(roomNode.Type));
                    x++;
                }
                y++;
            }
        }
    }

}
