using Match3.SubScripts;
using UnityEngine;

namespace Match3.Manager
{
    public enum EGameState
    {
        Normal, Pause, Start
    } 

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] CombatManager combat;
        public CombatManager Combat => CombatManager.Instance;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            combat.UpdateCombat();
        }

        public void Init()
        {
            combat.StartCombat();
        }
    }
}

