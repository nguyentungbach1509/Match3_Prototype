using Match3.SubScripts;
using UnityEngine;

namespace Match3.Manager
{
    public enum EGameState
    {
        Normal, Pause, Start, Lose, Win
    } 

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] CombatManager combat;

        private EGameState state;

        public EGameState State
        {
            get => state;
            set => state = value;
        }

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

