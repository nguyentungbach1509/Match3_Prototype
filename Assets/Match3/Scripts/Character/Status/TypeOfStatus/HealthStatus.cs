using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class HealthStatus : StatusEffect
    {
        public HealthStatus(StatusData data, CharacterStats target) : base(data, target)
        {
        }

        public override void OnApply(Action<int> OnCountChange, Action OnComplete, Image image = null, int multi=1)
        {
            DamageSetting(multi);
            stats.TakeDamage(-damage);
        }
    }
}

