using Match3.Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class LifeStealStatus : StatusEffect
    {
        private CombatManager combatManager => CombatManager.Instance;
        private Player player;

        public LifeStealStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            player = combatManager.LevelCtrl.Player;
        }

        public override void OnApply(Action<int> OnCountChange, Action OnComplete, Image image = null)
        {
            stats.TakeDamage(damage);
            player.Stats.TakeDamage(-damage);
        }
    }
}

