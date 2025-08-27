using Match3.Manager;
using SubScript.Pooling;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class CharacterBase : PoolableComponent
    {
        [SerializeField] protected StatsData data;
        [SerializeField] protected CharacterCanvas canvas;

        protected CombatManager combatManager => CombatManager.Instance;
        protected CharacterStats stats;
        protected Player player;

        public CharacterStats Stats => stats;
        public CharacterCanvas Canvas => canvas;
        public StatusController Status => canvas.StatusCtrl;

        public virtual void Init()
        {
            player = combatManager.LevelCtrl.Player;
            stats = new CharacterStats(data, canvas);
            canvas.StatusCtrl.Init(stats);
        }
    }
}

