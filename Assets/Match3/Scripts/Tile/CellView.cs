using SubScript.Pooling;
using UnityEngine;

namespace Match3.Scripts
{
    public class CellView : MonoBehaviour, IPoolable
    {
        [SerializeField] SpriteRenderer sprite;
        public SpriteRenderer Sprite => sprite;
        private int saveSortingOrder;

        public bool IsAbove() => sprite.sortingOrder > saveSortingOrder;
        public void IncreaseLayerOrder() => sprite.sortingOrder++;

        public void OnDespawn()
        {
            sprite.sortingOrder = saveSortingOrder;
        }

        public void OnSpawn()
        {
            saveSortingOrder = sprite.sortingOrder;
        }
    }
}

