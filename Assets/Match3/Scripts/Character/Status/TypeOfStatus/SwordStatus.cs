using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class SwordStatus : StatusEffect
    {
        public SwordStatus(StatusData data, CharacterStats target) : base(data, target)
        {
        }

        public override void OnApply(Action<int> OnCountChange, Action OnComplete, Image image = null)
        {
            stats.TakeDamage(damage);
        }
    }
}


