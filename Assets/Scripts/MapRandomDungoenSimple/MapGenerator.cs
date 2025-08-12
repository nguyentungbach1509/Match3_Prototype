
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Scripts.MapRandomDungoen
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] int count;
        [SerializeField] Room roomPrefab;

        [SerializeField] Tilemap tileRoom;
        [SerializeField] Grid grid;

        private List<Room> storeRooms = new List<Room>();

        private static readonly Direction[] directions = new Direction[] {
        new Direction(EDirection.Up, Vector2Int.up),
        new Direction(EDirection.Down, Vector2Int.down),
        new Direction(EDirection.Left, Vector2Int.left),
        new Direction(EDirection.Right, Vector2Int.right),
    };

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClearMap();
                SpawnRoom(GerateRoomNodes());
            }
        }

        private List<RoomNode> GerateRoomNodes()
        {
            var rooms = new List<RoomNode>();
            var occupiedPositions = new HashSet<Vector2Int>();

            RoomNode start = new RoomNode(Vector2Int.zero, RoomType.Start);
            start.Neighbors = new Dictionary<Direction, RoomNode>();
            rooms.Add(start);
            occupiedPositions.Add(Vector2Int.zero);
            int loop = 0;

            while (loop < count)
            {
                var currentRoom = rooms[Random.Range(0, rooms.Count)];
                var neighbors = currentRoom.Neighbors;
                Direction direct = directions[Random.Range(0, directions.Length)];
                if (neighbors == null) neighbors = new Dictionary<Direction, RoomNode>();
                Vector2Int neighborPos = currentRoom.Position + direct.DirectVector;

                // Kiểm tra vị trí đã có phòng chưa
                if (!neighbors.ContainsKey(direct) && !occupiedPositions.Contains(neighborPos))
                {
                    RoomNode neighborNode = new RoomNode(neighborPos, RoomType.Normal);
                    neighborNode.Neighbors = new Dictionary<Direction, RoomNode>();

                    neighbors.Add(direct, neighborNode);
                    neighborNode.Neighbors.Add(Opposite(direct), currentRoom);

                    rooms.Add(neighborNode);
                    occupiedPositions.Add(neighborPos);
                    loop++;
                }
            }

            SetUpBossRoom(rooms);
            SetUpShopRoom(rooms);

            return rooms;
        }

        private void SetUpShopRoom(List<RoomNode> rooms)
        {
            rooms = rooms.Skip(0);
            rooms.Shuffle();

            int shopRoom = Random.Range(1, 3);
            int shopCount = 0;

            for (int i = 0; i < rooms.Count && shopCount < shopRoom; i++)
            {
                if (rooms[i].Type == RoomType.Normal)
                {
                    rooms[i].SetType(RoomType.Shop);
                    shopCount++;
                }
            }
        }

        private void SetUpBossRoom(List<RoomNode> rooms)
        {
            var listRoom = rooms.Skip(0);
            RoomNode startRoom = rooms[0];

            float maxDistance = 0;
            RoomNode furthestRoom = listRoom[0];

            for (int i = 0; i < listRoom.Count; i++)
            {
                float distance = Vector2Int.Distance(startRoom.Position, listRoom[i].Position);
                if (maxDistance < distance)
                {
                    maxDistance = distance;
                    furthestRoom = listRoom[i];
                }
            }
            furthestRoom.SetType(RoomType.Boss);
        }

        private void SetUpBossRoom(RoomNode startRoom)
        {
            var visitedRooms = new HashSet<RoomNode>();
            var queue = new Queue<(RoomNode, int)>();
            queue.Enqueue((startRoom, 0));

            RoomNode furthestRoom = startRoom;
            int maxDist = 0;

            while (queue.Count > 0)
            {
                var (current, dist) = queue.Dequeue();

                if (visitedRooms.Contains(current)) continue;
                visitedRooms.Add(current);

                if (dist > maxDist)
                {
                    maxDist = dist;
                    furthestRoom = current;
                }

                foreach (var room in current.Neighbors)
                {
                    if (!visitedRooms.Contains(room.Value))
                    {
                        queue.Enqueue((room.Value, dist + 1));
                    }
                }
            }

            furthestRoom.SetType(RoomType.Boss);
        }

        private void SpawnRoom(List<RoomNode> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                Room room = Instantiate(roomPrefab, grid.transform);
                room.Init(tileRoom, rooms[i]);
                storeRooms.Add(room);
            }
        }

        private void ClearMap()
        {
            tileRoom.ClearAllTiles();
            foreach (var room in storeRooms) Destroy(room.gameObject);
            storeRooms.Clear();
        }

        private Direction Opposite(Direction direct)
        {
            switch (direct.Direct)
            {
                case EDirection.Left: return new Direction(EDirection.Right, Vector2Int.right);
                case EDirection.Right: return new Direction(EDirection.Left, Vector2Int.left);
                case EDirection.Up: return new Direction(EDirection.Down, Vector2Int.down);
                default: return new Direction(EDirection.Up, Vector2Int.up);
            }
        }
    }
}

