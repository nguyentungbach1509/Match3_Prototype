using Match3.Scripts.Character;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusSO", menuName = "CharacterSO/Status/StatusSO")]
public class StatusSO : ScriptableObject
{
    [SerializeField] float damage;
    [SerializeField] float time;
    [SerializeField] EStatus statusType;
    [SerializeField] Sprite statusIcon;

    public float Damage => damage;
    public float Time => time;
    public EStatus StatusType => statusType;
    public Sprite StatusIcon => statusIcon;

}
