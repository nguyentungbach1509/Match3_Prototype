using UnityEngine;

namespace Match3.Scripts.Character.StateMachine
{
    public class AtkState : State
    {
        public AtkState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("ATK!");
        }

        public override void OnExecute()
        {
                
        }

        public override void OnExit()
        {
                
        }
    }
}

