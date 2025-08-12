using System;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class DamageInfor
    {
        private string id;
        private float damage;
        private CharacterBase source;

        public string Id => id;
        public float Damage => damage;
        public CharacterBase Source => source;

        public DamageInfor(float dmg, CharacterBase character)
        {
            id = Guid.NewGuid().ToString(); 
            damage = dmg;
            source = character;
        }
    }
}

