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

        protected CharacterCanvas canvas;

        public int Id => id;
        public string NameKey => name;
        public float Damage => damage;
        public float MaxHp => maxHp;
        public float HP => hp;

        private Action<float> OnTakeDamage;
        private Action OnDie;

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
            hp -= damage;
            OnTakeDamage?.Invoke(hp/maxHp);
            if (damage <= 0)
            {
                hp = 0;
                OnDie?.Invoke();
            }
        }

        public void TakeDamage(DamageInfor damageInfor)
        {
            hp -= damageInfor.Damage;
            OnTakeDamage?.Invoke(hp / maxHp);
            if (damage <= 0)
            {
                hp = 0;
                OnDie?.Invoke();
            }
        }
    }
}

