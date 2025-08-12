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
        private Dictionary<EState, IState> dictStates;

        public StateController()
        {
            dictStates = new Dictionary<EState, IState>();

        }

        public void ChangeState()
        {

        }

        private void SelecetedState(EState stateType)
        {
            IState state;
            switch(stateType)
            {
                case EState.Idle:
                    
            }
        }
    }
}

