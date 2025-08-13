using DG.Tweening;
using SubScript.Pooling;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3.Scripts
{
    public class AnimateLayer : MonoBehaviour
    {
        [SerializeField] CellView movingTilePrefab; // prefab có SpriteRenderer
        public float swapDuration = 0.3f;


        private ObjectPool<CellView> cellViewPool;

        public void AnimateSwap(Grid grid, Cell cellA, Cell cellB, Action callBack)
        {
            Vector3 worldPosA = grid.CellToWorld(cellA.Position);
            Vector3 worldPosB = grid.CellToWorld(cellB.Position);

            var objA = SpawnMovingTile(worldPosA, cellA.Tile);
            var objB = SpawnMovingTile(worldPosB, cellB.Tile);

            if(!objA.IsAbove()&&!objB.IsAbove()) objA.IncreaseLayerOrder(); 

            objA.transform.DOMove(worldPosB, swapDuration).SetEase(Ease.InOutQuad);
            objB.transform.DOMove(worldPosA, swapDuration).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    callBack.Invoke();
                    cellViewPool.Despawn(objA);
                    cellViewPool.Despawn(objB);
                });
        }


        CellView SpawnMovingTile(Vector3 pos, Tile tile)
        {
            cellViewPool = PoolManager.CreateOrGetPool(movingTilePrefab);
            CellView go = cellViewPool.Spawn(pos, Quaternion.identity);
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(transform, false);
            go.Sprite.sprite = tile.sprite;
            return go;
        }

        public Tween AnimateMove(Grid grid, Cell cell, Vector3Int from, Vector3Int to, System.Action callBack)
        {
            Vector3 fromWorld = grid.CellToWorld(from);
            Vector3 toWorld = grid.CellToWorld(to);

            var view = SpawnMovingTile(fromWorld, cell.Tile);
            
            return view.transform.DOMove(toWorld, 0.3f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    callBack?.Invoke();
                    cellViewPool.Despawn(view);
                });
        }

        public Tween AnimateClear(Grid grid, Cell cell, Action callBack)
        {
            Vector3 spawnPos = grid.CellToWorld(cell.Position);
            var view = SpawnMovingTile(spawnPos, cell.Tile);
                
            // Scale to 0 để "nổ" biến mất
            return view.transform.DOScale(Vector3.zero, 0.15f)
                .OnComplete(() => {
                    callBack?.Invoke();
                    cellViewPool.Despawn(view);
                });
        }

    }
}

