using Match3.Scripts;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Match3.SubScripts
{
    public class MatchFinder
    {
        private int width;
        private int height;
        private Board board;

        private Func<Vector3Int, TileSquare> GetTileSquare;
        private HashSet<Vector3Int> matched;
        private Dictionary<ECellType, List<Vector3Int>> matchedGroup;

        public Dictionary<ECellType, List<Vector3Int>> MatchedGroup => matchedGroup;

        public MatchFinder(Board board)
        {
            this.board = board;
            width = board.Width;
            height = board.Height;
            GetTileSquare = board.GetTileSquare;
            matched = new HashSet<Vector3Int>();
            matchedGroup = new();
        }


        public HashSet<Vector3Int> FindAllMatches()
        {
            matched.Clear();
            matchedGroup.Clear();

            // Quét ngang
            for (int y = -height; y < 0; y++)
                ScanLine(matched, new Vector3Int(-width / 2, y, 0), Vector3Int.right, width);

            // Quét dọc
            for (int x = -width / 2; x < width / 2; x++)
                ScanLine(matched, new Vector3Int(x, -height, 0), Vector3Int.up, height);

            return matched;
        }

        public HashSet<Vector3Int> FindMatchesAt(Vector3Int pos)
        {
            matched.Clear();
            matchedGroup.Clear();

            // Hàng ngang qua pos
            ScanLine(matched,
                new Vector3Int(-width / 2, pos.y, 0), // start tại đầu hàng
                Vector3Int.right,
                width);

            // Cột dọc qua pos
            ScanLine(matched,
                new Vector3Int(pos.x, -height, 0), // start tại đầu cột
                Vector3Int.up,
                height);

            return matched;
        }


        private void ScanLine(HashSet<Vector3Int> matches, Vector3Int start, Vector3Int dir, int length)
        {
            int count = 1;
            Cell prev = GetTileSquare(start)?.Cell;

            for (int i = 1; i < length; i++)
            {
                Vector3Int pos = start + dir * i;
                Cell current = GetTileSquare(pos)?.Cell;

                if (current.Compare(prev)) count++;
                else
                {
                    if (count >= 3) AddRange(matches, pos - dir, dir, count);
                    count = 1;
                }

                prev = current;
            }

            // Check cuối dòng
            if (count >= 3) AddRange(matches, start + dir * (length - 1), dir, count);
            
        }

        private void AddRange(HashSet<Vector3Int> set, Vector3Int end, Vector3Int dir, int count)
        {
            ECellType cellType = GetTileSquare(end).Cell.CellType;

            if (!matchedGroup.ContainsKey(cellType)) matchedGroup[cellType] = new();

            for (int k = 0; k < count; k++)
            {
                Vector3Int pos = end - dir * k;
                set.Add(pos);
                matchedGroup[cellType].Add(pos);
            }
        }

    }
}

