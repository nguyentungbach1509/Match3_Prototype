using DG.Tweening;
using Match3.Subscripts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class TickStatus : StatusEffect
    {
        protected Sequence tickSequence;
        protected float elapsed; // dùng cho tính tick

        public TickStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

        public override void OnApply(Action<int> OnCountChange, Action OnComplete, Image image = null)
        {
            base.OnApply(OnCountChange, OnComplete);
            if (countStack > 0)
            {
                OnTick(OnCountChange, OnComplete, image);
            }
        }

        /// <summary>
        /// Tick damage + fill UI (Mode B: reset fill mỗi stack)
        /// </summary>
        protected override void OnTick(Action<int> OnCountChange, Action OnComplete, Image image = null)
        {
            base.OnTick(OnCountChange, OnComplete);
            tickSequence?.Kill();
            tickSequence = DOTween.Sequence();
            elapsed = 0f;

            tickSequence.Append(
                image.DOFill(0, duration)
                        .SetEase(Ease.Linear)
                        .SetLoops(countStack, LoopType.Restart)
                        .OnUpdate(() =>
                        {
                            // damage tick
                            elapsed += Time.deltaTime;
                            if (elapsed >= Tick)
                            {
                                stats.TakeDamage(damagePerTick);
                                elapsed -= Tick;
                                OnCountChange?.Invoke(countStack);
                            }
                        })
            )
            .OnComplete(() =>
            {
                OnExpired(OnComplete);
                OnComplete?.Invoke();
            });
        }

        protected override void OnExpired(Action OnCompleted)
        {
            tickSequence?.Kill();
            tickSequence = null;
            countStack = 0;
        }
    }
}

