using UnityEngine;

namespace Match3.Scripts.Character
{
    public class State : IState
    {
        protected EnemyBase enemy;
        
        public State(EnemyBase enemy)
        {
            this.enemy = enemy;
        }

        public void OnEnter()
        {
            
        }

        public void OnExecute()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}

