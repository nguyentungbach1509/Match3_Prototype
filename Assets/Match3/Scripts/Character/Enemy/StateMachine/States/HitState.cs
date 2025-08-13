using UnityEngine;

namespace Match3.Scripts.Character.StateMachine
{
    public class HitState : State
    {
        public HitState(EnemyBase enemy) : base(enemy)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Hit!");
        }

        public override void OnExit()
        {
            
        }

        public override void OnExecute()
        {
            
        }
    }
}

