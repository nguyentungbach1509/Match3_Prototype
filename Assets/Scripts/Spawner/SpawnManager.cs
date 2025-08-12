using SubScript.GamePrefabController.Data;
using SubScript.Singleton;
using SubScript.VFXEffect;
using UnityEngine;

namespace Subscript.Spawner
{
    public class SpawnManager : SingletonBase<SpawnManager>
    {
        [SerializeField] GamePrefabSO gamePrefabSO; 

        private VFXSpawner vfxSpawner;
        
        public VFXSpawner VFXSpawner => vfxSpawner;
        
        
        public void Init()
        {
            vfxSpawner = new VFXSpawner(gamePrefabSO);
        }


    }
}

