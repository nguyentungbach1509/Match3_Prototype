using System.Collections.Generic;
using UnityEngine;
using SubScript.VFXEffect;
using Match3.Scripts.Spawner;


namespace SubScript.GamePrefabController.Data
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "Game/Prefabs")]
    public class GamePrefabSO : ScriptableObject
    {
        [Header("Vfx Prefabs")]
        public List<VFXData> VfxPrefabs = new List<VFXData>();

        [Header("Enemy Prefabs")]
        public List<EnemySpawnerData> EnemyPrefabs = new List<EnemySpawnerData>();

        [Header("Enemy Spawn Settings")]
        public SpawnSettings Settings;
    }

}


