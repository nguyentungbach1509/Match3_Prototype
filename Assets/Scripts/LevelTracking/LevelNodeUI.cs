using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelTracking
{
    public enum State
    {
        Locked,
        Current,
        Completed
    }


    public class LevelNodeUI : MonoBehaviour
    {
        public TMP_Text levelText;
        public Image statusIcon;

       
        public Image icon;
        public Color lockedColor, currentColor, completedColor;
        
        public void Setup(int levelNumber, bool isReached)
        {
            levelText.text = levelNumber.ToString();
            statusIcon.enabled = isReached;
        }

        public void SetState(State state)
        {
            switch (state)
            {
                case State.Locked:
                    icon.color = lockedColor;
                    break;
                case State.Current:
                    icon.color = currentColor;
                    break;
                case State.Completed:
                    icon.color = completedColor;
                    break;
            }
        }
    }
}

