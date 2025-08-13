using Match3.Scripts.Character.StateMachine;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public enum EState
    {
        Idle, Atk, Hit, Die
    }

    public class StateController 
    {
        private IState currentState;
        private Dictionary<EState, IState> cachedStates;
        private EnemyBase enemy;

        public StateController(EnemyBase enemy)
        {
            cachedStates = new Dictionary<EState, IState>();
            this.enemy = enemy;
        }

        public void ChangeState(EState stateType)
        {
            currentState.OnExit();
            if(cachedStates.TryGetValue(stateType, out IState state)) currentState = state;
            else currentState = SelecetedState(stateType);
            currentState.OnEnter();
        }

        private IState SelecetedState(EState stateType)
        {
            IState state;
            switch(stateType)
            {
                case EState.Idle:
                    state = new IdleState(enemy);
                    AddState(stateType, state);
                    break;
                case EState.Atk:
                    state = new AtkState(enemy);
                    AddState(stateType, state);
                    break;
                case EState.Hit:
                    state = new HitState(enemy);
                    AddState(stateType, state);
                    break;
                default:
                    state = new DieState(enemy);
                    AddState(stateType, state);
                    break;
                    
            }

            return state;
        }

        private void AddState(EState key, IState value)
        {
            if (cachedStates.ContainsKey(key)) return;
            cachedStates.Add(key, value);
        }
    }
}

