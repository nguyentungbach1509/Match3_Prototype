using DG.Tweening;
using Match3.Subscripts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class BurnStatus : TickStatus
    {
        

        public BurnStatus(StatusData data, CharacterStats target) : base(data, target)
        {
            applyOnUI = true;
        }

    }
}

