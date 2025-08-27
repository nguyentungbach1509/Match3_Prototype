using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class ArmorStatus : CounterStatus
    {
        public ArmorStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

    }
}


