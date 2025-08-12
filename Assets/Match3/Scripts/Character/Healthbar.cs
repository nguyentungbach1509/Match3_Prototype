using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image fillImg;
    [SerializeField] float smoothTime;

    public void UpdateHp(float hp)
    {
        fillImg.fillAmount = hp;
    }
}
