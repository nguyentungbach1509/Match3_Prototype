using Match3.Scripts.Character;
using UnityEngine;

namespace Match3.Scripts.Spawner
{
    [CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Spawner/Data/EnemySpawnerData")]
    public class EnemySpawnerData : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] EnemyBase prefab;
        [SerializeField] EEnemyTier tier;

        public EnemyBase Prefab => prefab;
        public string Key => key;
        public EEnemyTier Tier => tier;
    }
}

