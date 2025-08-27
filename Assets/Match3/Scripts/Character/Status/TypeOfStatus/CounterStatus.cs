using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class CounterStatus : StatusEffect
    {
        public CounterStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

        public override void OnApply(Action<int> OnCountChange, Action OnComplete, Image image = null)
        {
            timeRemaining = duration;
            countStack += (int)damage;
            OnCountChange(countStack);
        }
    }
}

