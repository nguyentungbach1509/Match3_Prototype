using DG.Tweening;
using Match3.Manager;
using Match3.Scripts.Character;
using Match3.Scripts.Level;
using Match3.Subscripts;
using Match3.SubScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3.Scripts
{
    public class Board : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] Tilemap tileMapBG;
        [SerializeField] Tile tileBg;
        [SerializeField] Tilemap tileMapCells;

        [SerializeField] int width;
        [SerializeField] int height;

        private AnimateLayer animLayer;

        private Dictionary<Vector3Int, TileSquare> boardDict;
        private InputManager input => InputManager.Instance;
        private bool isInit;
        private TileSquare selectSquare;

        private MatchFinder matchFinder;
        private Coroutine matchCoroutine;

        private EnemyBase enemy;
        private Player player;

        private LevelController levelCtrl;

        int minX, maxX, minY, maxY;

        public int Width => width;
        public int Height => height;

        public void Init(LevelController level, AnimateLayer layer, EnemyBase enemy)
        {
            matchFinder = new MatchFinder(this);
            levelCtrl = level;
            player = level.Player;
            boardDict = new();
            animLayer = layer;
            this.enemy = enemy;

            minX = -width / 2;
            maxX = width / 2 - 1;

            minY = -height;
            maxY = -1;

            for (int x = -width/2; x <= maxX; x++)
            {
                for(int y =minY; y <= maxY; y++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    tileMapBG.SetTile(position, tileBg);
                    TileSquare tileSquare = new TileSquare(position);
                    tileMapCells.SetTile(position, tileSquare.Cell.Tile);
                    
                    boardDict.Add(position, tileSquare);
                }
            }
            isInit = true;
            ProgressMatches();
        }

        public void UpdateBoard()
        {
            if (input.Disable) return;
            input.UpdateInput();
            if (!isInit) return;
            SwapTileCell();
        }

        public void SwapTileCell()
        {
            if (input.IsLeftMouseButtonDown)
            {
                if (boardDict.TryGetValue(input.MousePos(), out var tileSquare))
                    selectSquare = tileSquare;
            }

            if (input.IsDragging) return;

            if (input.IsMouseButtonUp)
            {
                if (!boardDict.TryGetValue(input.MousePos(), out var targetSquare))
                    return;

                if (!selectSquare.IsNeighbour(targetSquare)) return;

                // 1. Clear tilemap gốc
                tileMapCells.SetTile(selectSquare.Position, null);
                tileMapCells.SetTile(targetSquare.Position, null);

                // 2. Check hợp lệ trước
                bool valid = IsSwapable(targetSquare);

                // 3. Animate swap
                animLayer.AnimateSwap(grid, selectSquare.Cell, targetSquare.Cell, () =>
                {
                    if (valid)
                    {
                        // Swap data và set tile mới
                        selectSquare.SwapCell(targetSquare);
                        tileMapCells.SetTile(selectSquare.Position, selectSquare.Cell.Tile);
                        tileMapCells.SetTile(targetSquare.Position, targetSquare.Cell.Tile);
                        if(selectSquare.Cell.SubType==ECellType.Target || 
                        targetSquare.Cell.SubType==ECellType.Target)
                        {
                            ProgressMatchesTarget(selectSquare.Cell.SubType == ECellType.Target ?
                                targetSquare.Cell.CellType : selectSquare.Cell.CellType);
                        }
                        else ProgressMatches();
                    }
                    else
                    {
                        // Animate trả về
                        tileMapCells.SetTile(selectSquare.Position, null);
                        tileMapCells.SetTile(targetSquare.Position, null);

                        animLayer.AnimateSwap(grid, targetSquare.Cell, selectSquare.Cell, () =>
                        {
                            tileMapCells.SetTile(selectSquare.Position, selectSquare.Cell.Tile);
                            tileMapCells.SetTile(targetSquare.Position, targetSquare.Cell.Tile);
                        });
                    }
                });
            }
        }

        private bool IsSwapable(TileSquare square)
        {
            if(selectSquare.Cell.SubType == ECellType.Target || 
                square.Cell.SubType == ECellType.Target)  return true; 
            // Swap tạm
            selectSquare.SwapCell(square);

            bool valid = matchFinder.FindMatchesAt(selectSquare.Position).Count >= 3 ||
                         matchFinder.FindMatchesAt(square.Position).Count >= 3;

            // Swap trả lại
            square.SwapCell(selectSquare);

            return valid;
        }


        private TileSquare GetNeighbour(Vector3Int position, EDirection direct)
        {
            Vector3Int target = position + Match3Sub.Directs[direct];
            return boardDict.TryGetValue(target, out var neighbour) ? neighbour : null;
        }

        void UpdateNeighbours(TileSquare tileSquare)
        {
            tileSquare.Top = GetNeighbour(tileSquare.Position, EDirection.Up);
            tileSquare.Down = GetNeighbour(tileSquare.Position, EDirection.Down);
            tileSquare.Left = GetNeighbour(tileSquare.Position, EDirection.Left);
            tileSquare.Right = GetNeighbour(tileSquare.Position, EDirection.Right);
        }

        public TileSquare GetTileSquare(Vector3Int pos)
        {
            return boardDict.TryGetValue(pos, out var tileSquare) ? tileSquare:null;
        }

        public void ProgressMatches()
        {
            if(matchCoroutine != null) StopCoroutine(matchCoroutine);
            matchCoroutine = StartCoroutine(Progress());

            IEnumerator Progress()
            {
                input.Disable = true;
                while (true)
                {
                    var matches = matchFinder.FindAllMatches();
                    if (matches.Count <= 0) break;

                    // Áp dụng hiệu ứng dựa trên loại ô vừa match
                    ApplyMatchEffects();
                    yield return ClearMatches(matches);

                    yield return GravityDrop();
                    yield return SpawnNewTiles();
                }
                input.Disable = false;
            }
        }

        public void ProgressMatchesTarget(ECellType type)
        {
            if (matchCoroutine != null) StopCoroutine(matchCoroutine);
            matchCoroutine = StartCoroutine(Progress());

            IEnumerator Progress()
            {
                input.Disable = true;
                while (true)
                {
                    var matches = matchFinder.FindAllTargetMatches(type);
                    if (matches.Count <= 0) break;

                    // Áp dụng hiệu ứng dựa trên loại ô vừa match
                    ApplyMatchEffects();
                    yield return ClearMatches(matches);

                    yield return GravityDrop();
                    yield return SpawnNewTiles();
                }
                input.Disable = false;
            }
        }

        private void ApplyMatchEffects()
        {
            if (enemy == null) return;
            var groups = matchFinder.MatchedGroup;
            foreach (var kv in groups)
            {
                var positions = kv.Value;
                if (positions == null || positions.Count == 0) continue;
                int goDouble = 1;
                for(int i = 0; i < positions.Count; i++)
                {
                    if (boardDict[positions[i]].Cell.SubType == ECellType.Double)
                    {
                        goDouble = 2;
                        break;
                    }
                }
                var representativePos = positions[0];
                if (!boardDict.TryGetValue(representativePos, out var square)) continue;
                if (square == null || square.Cell == null) continue;
                if (square.Cell.CellType != ECellType.Health &&
                    square.Cell.CellType != ECellType.Shield &&
                    square.Cell.CellType != ECellType.Cloak) enemy.Status.Apply(square.Cell.CellType);

                else player.Status.Apply(square.Cell.CellType, goDouble); 
            }
        }

        private IEnumerator ClearMatches(HashSet<Vector3Int> matches)
        {
            Sequence sequence = DOTween.Sequence();

            foreach(var match in matches)
            {
                if(!boardDict.TryGetValue(match, out var square)) continue;
                if(square == null) continue;
                
                var anim = animLayer.AnimateClear(grid, square.Cell, () =>
                {
                    square.SetCell(null);
                    tileMapCells.SetTile(match, null);
                });
                sequence.Join(anim);
            }

            foreach(var group in matchFinder.MatchedGroup)
            {
                if(group.Value.Count > 3)
                {
                    (ECellType, Vector3Int) specialCell = matchFinder.GetSpecialCell(group.Key);
                    Cell newCell = new Cell(boardDict[specialCell.Item2], specialCell.Item2, specialCell.Item1);
                    var anim = animLayer.AnimateMove(grid, newCell, specialCell.Item2, specialCell.Item2, () =>
                    {
                        boardDict[specialCell.Item2].SetCell(newCell);
                        tileMapCells.SetTile(specialCell.Item2, newCell.Tile);
                        newCell.SetCellType(group.Key);
                    });

                    sequence.Join(anim);
                }
            }

            yield return sequence.WaitForCompletion();
            //matches.Clear();
        }

        private IEnumerator GravityDrop()
        {
            Sequence sequence = DOTween.Sequence();

            // cho mỗi cột
            for (int x = minX; x <= maxX; x++)
            {
                // gom cell hiện có trong cột, từ bottom -> top
                List<Cell> cells = new List<Cell>();
                for (int y = minY; y <= maxY; y++)
                {
                    var pos = new Vector3Int(x, y, 0);
                    var sq = boardDict[pos];
                    if (sq.Cell != null)
                    {
                        cells.Add(sq.Cell);
                        sq.SetCell(null);
                        tileMapCells.SetTile(pos, null); // sẽ cập nhật lại khi đặt xuống
                    }
                }

                // đặt lại từ bottom
                int writeY = minY;
                foreach (var cell in cells)
                {
                    Vector3Int initPos = cell.Position;
                    var dest = new Vector3Int(x, writeY, 0);

                    var anim = animLayer.AnimateMove(grid, cell, initPos, dest, () =>
                    {
                        boardDict[dest].SetAndAssignCell(cell);
                        tileMapCells.SetTile(dest, cell.Tile);
                        // nếu muốn animate rơi: spawn animate view tại old world pos -> move to dest world pos

                    });
                    
                    sequence.Join(anim);
                    writeY++;
                }
            }

            yield return sequence.WaitForCompletion();
        }

        public IEnumerator SpawnNewTiles()
        {

            Sequence seq = DOTween.Sequence();

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var pos = new Vector3Int(x, y, 0);
                    var sq = boardDict[pos];
                    if (sq.Cell == null)
                    {
                        Cell newCell = new Cell(sq, pos);
                        sq.SetCell(newCell);

                        // Gọi AnimateLayer để spawn từ trên cao
                        int extraHeight = Random.Range(1, 4);
                        Vector3Int spawnPos = new Vector3Int(x, maxY+extraHeight, 0); // spawn từ hàng trên cùng
                        var anim = animLayer.AnimateMove(grid, newCell, spawnPos, pos, () =>
                        {
                            tileMapCells.SetTile(pos, newCell.Tile);
                        });

                        seq.Join(anim);
                    }
                }
            }

            yield return seq.WaitForCompletion();
        }

    }
}

