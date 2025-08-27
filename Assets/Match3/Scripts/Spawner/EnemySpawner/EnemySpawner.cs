using Match3.Scripts.Character;
using SubScript.GamePrefabController.Data;
using SubScript.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts.Spawner
{
    public enum EEnemyTier
    {
        Tier1, Tier2, Tier3, Tier4,
    }

    public class EnemySpawner
    {
        private GamePrefabSO gamePrefabSO;

        private Dictionary<string, ObjectPool<PoolableComponent>> enemyDictPool;
        private bool isInitialized = false;

        private SpawnSettings settings;

        public bool IsInitialized() => isInitialized;

        public EnemySpawner(GamePrefabSO so)
        {
            if (isInitialized) return;

            gamePrefabSO = so;
            settings = so.Settings;
            Debug.Log("Bắt đầu khởi tạo EnemyManager...");

            InitPoolEnemy();

            isInitialized = true;
            Debug.Log("EnemyManager khởi tạo hoàn tất");
        }

        private void InitPoolEnemy()
        {
            enemyDictPool = new Dictionary<string, ObjectPool<PoolableComponent>>();

            if (gamePrefabSO == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var enemyData in gamePrefabSO.EnemyPrefabs)
            {
                string key = enemyData.Key.ToString();
                Debug.Log("Khởi tạo VFX pool: " + key);
                EnemyBase enemyPrefab = enemyData.Prefab;
                ObjectPool<PoolableComponent> pool = PoolManager.CreateOrGetPool<EnemyBase>(enemyPrefab, key);
                enemyDictPool.Add(key, pool);
            }
        }

        public EnemyBase SpawnEnemy(string key, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            EnemyBase enemy = enemyDictPool[key].Spawn(position, rotation) as EnemyBase;
            if (parent != null) enemy.transform.SetParent(parent, true);
            enemy.Init();
            return enemy;
        }

        private List<string> GetTierKeys(EEnemyTier tier)
        {
            List<string> keys = new List<string>();
            foreach (var prefab in gamePrefabSO.EnemyPrefabs)
            {
                if (prefab.Tier == tier) keys.Add(prefab.Key);
            }
            return keys;
        }

        public void SpawnRandomEnemy(int currentWave, Vector3 position, Quaternion rotation)
        {

            //tinh int level = room.level + dungeon.level;
            //int percent = Mathf.Clamp01((float)level / maxLevel);
            //float percent = currentWave / 5f;
            float tier1Chance = settings.GetTierChance(EEnemyTier.Tier1, currentWave);
            float tier2Chance = settings.GetTierChance(EEnemyTier.Tier2, currentWave);
            float tier3Chance = settings.GetTierChance(EEnemyTier.Tier3, currentWave);

            float totalChance = tier1Chance + tier2Chance + tier3Chance;

            tier1Chance /= totalChance;
            tier2Chance /= totalChance;
            tier3Chance /= totalChance;

            List<string> tier3Keys = GetTierKeys(EEnemyTier.Tier3);
            List<string> tier1Keys = GetTierKeys(EEnemyTier.Tier1);
            List<string> tier2Keys = GetTierKeys(EEnemyTier.Tier2);

            float randomValue = Random.value;

            if (randomValue < tier1Chance)
            {
                SpawnEnemy(tier1Keys[Random.Range(0, tier1Keys.Count)], position, rotation);
                return;
            }
            if (randomValue < tier1Chance + tier2Chance)
            {
                SpawnEnemy(tier2Keys[Random.Range(0, tier2Keys.Count)], position, rotation);
                return;
            }
            SpawnEnemy(tier3Keys[Random.Range(0, tier3Keys.Count)], position, rotation);
        }

        public void DespawnEnemy(string key, EnemyBase enemy)
        {
            enemyDictPool[key].Despawn(enemy);
        }
    }
}

