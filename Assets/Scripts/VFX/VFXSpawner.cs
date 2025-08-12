using SubScript.GamePrefabController.Data;
using SubScript.Pooling;
using System.Collections.Generic;
using UnityEngine;


namespace SubScript.VFXEffect
{
    public class VFXSpawner 
    {
        private GamePrefabSO gamePrefabSO;
        
        private Dictionary<string, ObjectPool<PoolableComponent>> vfxDictPool;
        private bool isInitialized = false;
        
        public bool IsInitialized() => isInitialized;

        public VFXSpawner(GamePrefabSO so)
        {
            if (isInitialized) return;
            
            gamePrefabSO = so;
            Debug.Log("Bắt đầu khởi tạo VFXManager...");
            
            InitPoolVFX();
            
            isInitialized = true;
            Debug.Log("VFXManager khởi tạo hoàn tất");
        }
        
        private void InitPoolVFX()
        {
            vfxDictPool = new Dictionary<string, ObjectPool<PoolableComponent>>();
            
            if (gamePrefabSO == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }
            
            foreach (var vfxData in gamePrefabSO.VfxPrefabs)
            {
                string key = vfxData.Key.ToString();
                Debug.Log("Khởi tạo VFX pool: " + key);
                VFX vfxPrefab = vfxData.Prefab;
                ObjectPool<PoolableComponent> pool = PoolManager.CreateOrGetPool<VFX>(vfxPrefab, key);
                vfxDictPool.Add(key, pool);
            }
        }

        public VFX SpawnVFX(string key, Vector3 position, Quaternion rotation)
        {
            if (!vfxDictPool.ContainsKey(key))
            {
                Debug.LogError($"VFX với key {key} không tồn tại trong pool!");
                return null;
            }
            
            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            VFX vfx = pool.Spawn(position, rotation) as VFX;
            pool.DespawnDelayedNoWait(vfx, vfx.EndOffsetTime);
            return vfx;
        }

        public VFX SpawnVFX(string key, Vector3 position, Quaternion rotation, System.Action onVFXSpawned = null)
        {
            if (!vfxDictPool.ContainsKey(key))
            {
                Debug.LogError($"VFX với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            VFX vfx = pool.Spawn(position, rotation) as VFX;

            onVFXSpawned?.Invoke();

            return vfx;
        }

        public VFX SpawnVFX(string key, Vector3 position, Quaternion rotation, Transform container, System.Action<VFX> onVFXSpawned = null)
        {
            if (!vfxDictPool.ContainsKey(key))
            {
                Debug.LogError($"VFX với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            VFX vfx = pool.Spawn(position, rotation) as VFX;
            pool.DespawnDelayedNoWait(vfx, vfx.EndOffsetTime);
            vfx.transform.parent = container;

            onVFXSpawned?.Invoke(vfx);
            return vfx;
        }

        public VFX SpawnVFX(string key, float hideTime, Transform container, Quaternion rotation, System.Action<VFX> onVFXSpawned = null)
        {
            if (!vfxDictPool.ContainsKey(key))
            {
                Debug.LogError($"VFX với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            VFX vfx = pool.Spawn(container.position, rotation) as VFX;
            pool.DespawnDelayedNoWait(vfx, hideTime);
            vfx.transform.parent = container;

            onVFXSpawned?.Invoke(vfx);

            return vfx;
        }

        public VFX SpawnVFX(string key, float hideTime, Vector3 position, Quaternion rotation, System.Action<VFX> onVFXSpawned = null)
        {
            if (!vfxDictPool.ContainsKey(key))
            {
                Debug.LogError($"VFX với key {key} không tồn tại trong pool!");
                return null;
            }
            
            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            VFX vfx = pool.Spawn(position, rotation) as VFX;
            pool.DespawnDelayedNoWait(vfx, hideTime);
            
            onVFXSpawned?.Invoke(vfx);
            
            return vfx;
        }

        public void DespawnVFX(string key, VFX vfx)
        {
            ObjectPool<PoolableComponent> pool = vfxDictPool[key];
            pool.Despawn(vfx);
        }
    }
}

