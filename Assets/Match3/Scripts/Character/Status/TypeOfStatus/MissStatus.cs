using System;
using UnityEngine;
using UnityEngine.UI;
namespace Match3.Scripts.Character
{
    public class MissStatus : CounterStatus
    {
        
        public MissStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

    }
}

