using Match3.Subscripts;
using System;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class CharacterStats
    {
        protected int id;
        protected string name;
        protected float damage;
        protected float maxHp;
        protected float hp;
        protected int armor;

        protected CharacterCanvas canvas;

        public int Id => id;
        public string NameKey => name;
        public float Damage => damage;
        public float MaxHp => maxHp;
        public float HP => hp;
        public int Armor => armor;    

        private Action<float> OnTakeDamage;
        private Action OnDie;
        public Action<int> OnCountChange;

        public CharacterStats(StatsData data, CharacterCanvas canvas)
        {
            this.canvas = canvas;
            id = data.Id;
            name = data.NameKey;
            damage = data.Damage;
            maxHp = data.MaxHp;
            hp = maxHp;
            OnTakeDamage += canvas.HpBar.UpdateHp;
        }

        public void TakeDamage(float damage)
        {
            if(CheckStatusBeforeTakeDamage()) return;
            hp = Mathf.Clamp(hp-damage, 0, maxHp);
            OnTakeDamage?.Invoke(hp/maxHp);
            if (hp <= 0)OnDie?.Invoke();
        }

        public void TakeDamage(DamageInfor damageInfor)
        {
            if(CheckStatusBeforeTakeDamage()) return;
            hp = Mathf.Clamp(hp - damageInfor.Damage, 0, maxHp);
            OnTakeDamage?.Invoke(hp / maxHp);
            if (hp <= 0)OnDie?.Invoke();
        }

        private bool CheckStatusBeforeTakeDamage()
        {
            bool preventDamage = canvas.StatusCtrl.GotStatus(ECellType.Cloak) ||
                canvas.StatusCtrl.GotStatus(ECellType.Shield);
            if (!preventDamage) return false;
            StatusEffect effect = null;
            if (canvas.StatusCtrl.GotStatus(ECellType.Cloak)) effect = canvas.StatusCtrl.GetStatus(ECellType.Cloak);
            else if (canvas.StatusCtrl.GotStatus(ECellType.Shield)) effect = canvas.StatusCtrl.GetStatus(ECellType.Shield);
            effect.CountStack = Mathf.Clamp(effect.CountStack-1, 0, effect.CountStack);
            OnCountChange?.Invoke(effect.CountStack);
            return true;
        }
    }
}

