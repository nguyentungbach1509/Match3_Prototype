using UnityEngine;

namespace Match3.Scripts.Character.StateMachine
{
    public class IdleState : State
    {
        public IdleState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Idle!");
        }

        public override void OnExecute()
        {

        }

        public override void OnExit()
        {

        }
    }
}

