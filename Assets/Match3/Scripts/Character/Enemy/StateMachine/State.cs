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

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExecute()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}

