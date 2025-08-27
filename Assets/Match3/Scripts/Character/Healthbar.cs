using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image fillImg;
    [SerializeField] float smoothTime;

    public void UpdateHp(float hp)
    {
        fillImg.DOKill(); // huỷ tween cũ trên fillImg trước
        fillImg.DOFillAmount(hp, smoothTime).SetEase(Ease.OutCubic);
    }
}
