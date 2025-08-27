using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{ 
    public class IceStatus : TickStatus
    {
        
        public IceStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

    }
}


