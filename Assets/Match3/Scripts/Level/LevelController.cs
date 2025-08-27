using Match3.Scripts.Character;
using Match3.Subscripts;
using Subscript.Spawner;
using UnityEngine;

namespace Match3.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] BoardController boardcontroller;
        [SerializeField] Player player;

        [SerializeField] Transform enemyContainer;

        private bool isInitialize;
        private EnemyBase enemy;

        public Player Player => player;

        public void Init(SpawnManager spawn)
        {
            enemy = spawn.EnemySpawner.SpawnEnemy(Constants.Normal_Enemy, 
                enemyContainer.position, Quaternion.identity, enemyContainer);
            boardcontroller.Init(this, enemy);
            player.Init();
            isInitialize = true;
        }

        public void UpdateLevel()
        {
            if (!isInitialize) return;
            enemy.UpdateState();
            if (player.Status.GotFrozen()) return;
            boardcontroller.UpdateBoard();
        }
    }
}

