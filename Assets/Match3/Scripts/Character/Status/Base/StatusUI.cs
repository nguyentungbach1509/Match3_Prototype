using DG.Tweening;
using Match3.Subscripts;
using SubScript.Pooling;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Scripts.Character
{
    public class StatusUI : PoolableComponent
    {
        [SerializeField] Image fillImg;
        [SerializeField] Image blurImg;
        [SerializeField] TMP_Text countTxt;

        private CharacterStats stats;
        private StatusController controller;
        private ECellType cellType;

        public void Apply(StatusEffect status, StatusController controller, CharacterStats charStats, int multi=1)
        {
            fillImg.sprite = status.Icon;
            blurImg.sprite = status.Icon;
            this.controller = controller;
            cellType = status.Type;
            status.OnApply(UpdateCounter, () => RemoveUI(status.Type, controller), fillImg, multi);
            charStats.OnCountChange += UpdateCounter;
            stats = charStats;
        }

        private void UpdateCounter(int value)
        {
            countTxt.text = value.ToString();
            if (value <= 0) RemoveUI(cellType, controller);
        }

        private void RemoveUI(ECellType type, StatusController controller)
        {
            stats.OnCountChange -= UpdateCounter;
            controller.RemoveStatus(type, this);
        }
    }
}


