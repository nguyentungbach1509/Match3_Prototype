using Match3.Scripts.Level;
using Match3.SubScripts;
using Subscript.Spawner;
using UnityEngine;

namespace Match3.Manager
{
    public class CombatManager : Singleton<CombatManager>   
    {
        [SerializeField] LevelController levelController;
        [SerializeField] SpawnManager spawner;

        public LevelController LevelCtrl => levelController;

        public void StartCombat()
        {
            spawner.Init();
            levelController.Init(spawner);
        }

        public void UpdateCombat()
        {
            levelController.UpdateLevel();
        }
    }
}

