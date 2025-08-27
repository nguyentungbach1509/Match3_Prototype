using Match3.Scripts.Spawner;
using SubScript.GamePrefabController.Data;
using SubScript.Singleton;
using SubScript.VFXEffect;
using UnityEngine;

namespace Subscript.Spawner
{
    public class SpawnManager : SingletonBase<SpawnManager>
    {
        [SerializeField] GamePrefabSO gamePrefabSO;

        private EnemySpawner enemySpawner;
        private VFXSpawner vfxSpawner;
        
        public VFXSpawner VFXSpawner => vfxSpawner;
        public EnemySpawner EnemySpawner => enemySpawner;
        
        public void Init()
        {
            //vfxSpawner = new VFXSpawner(gamePrefabSO);
            enemySpawner = new EnemySpawner(gamePrefabSO);
        }

    }
}

