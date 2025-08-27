using Match3.Scripts.Character;
using Match3.Subscripts;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Data/Status/StatusData")]
public class StatusData : ScriptableObject
{
    [SerializeField] ECellType cellType;
    [SerializeField] float damage;
    [SerializeField] float duration;
    [SerializeField] float tick;
    [SerializeField] float damagePerTick;
    [SerializeField] Sprite icon;

    public ECellType Type => cellType;
    public float Damage => damage;
    public float Duration => duration;
    public float Tick => tick;
    public float DamagePerTick => damagePerTick;
    public Sprite Icon => icon;
    public StatusEffect CreateEffect(CharacterStats character)
    {
        switch (cellType)
        {
            case ECellType.Sword:
                return new SwordStatus(this, character);
            case ECellType.Shield:
                return new ArmorStatus(this, character);
            case ECellType.Fire:
                return new BurnStatus(this, character);
            case ECellType.Skull:
                return new LifeStealStatus(this, character);
            case ECellType.Cloak:
                return new MissStatus(this, character);
            case ECellType.Ice:
                return new IceStatus(this, character);
            default:
                return new HealthStatus(this, character);
        }
    }
}


