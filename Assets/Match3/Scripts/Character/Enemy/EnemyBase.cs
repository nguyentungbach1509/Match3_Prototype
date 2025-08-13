using Scripts;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public class EnemyBase : CharacterBase
    {
        [SerializeField] protected AnimationController anim;
        public AnimationController Anim => anim;
    }
}

