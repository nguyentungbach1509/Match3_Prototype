using Unity.VisualScripting;
using UnityEngine;

namespace Match3.Scripts.Character
{
    public interface IState
    {
        void OnEnter();
        void OnExecute();
        void OnExit();
    }
}

