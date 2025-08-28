using Match3.Scripts.Character;
using Match3.Subscripts;
using System;
using UnityEngine;
using UnityEngine.UI;


public class StatusEffect
{
    protected ECellType cellType;
    protected float damage;
    protected float duration;
    protected float tick;
    protected float damagePerTick;
    protected StatusData data;
    protected CharacterStats stats;
    protected int countStack;
    protected float timeRemaining;

    protected bool applyOnUI;
    protected float saveDmg;
    protected float saveDmgPerTick;

    public float Damage => damage;

    public float Duration => duration;

    public float Tick => tick;
    
    public float DamagePerTick => damagePerTick;
        
    public ECellType Type => cellType;
    public Sprite Icon => data.Icon;
    
    public int CountStack
    {
        get => countStack;
        set => countStack = value;
    }
    
    public Action OnEffectExpired;
    public bool ApplyOnUI => applyOnUI;
    public StatusEffect(StatusData data, CharacterStats target)
    {
        stats = target;
        cellType = data.Type;
        damage = data.Damage;
        duration = data.Duration;
        tick = data.Tick;
        damagePerTick = data.DamagePerTick;
        this.data = data;
        saveDmg = damage;
        saveDmgPerTick = damagePerTick;
    }

    public virtual void OnApply(Action<int> OnCountChange, Action OnComplete, Image fillImg=null, int multi=1)
    {
        countStack++;
        DamageSetting(multi);
        timeRemaining = duration;
        OnCountChange(countStack);
    }

    protected virtual void OnTick(Action<int> OnCountChange, Action OnComplete, Image fillImg=null) { }

    protected virtual void OnExpired(Action OnCompleted) { }

    protected virtual void DamageSetting(int multi=1)
    {
        if (multi > 1)
        {
            damage *= multi;
            damagePerTick *= multi;
        }
        else
        {
            damage = saveDmg;
            damagePerTick = saveDmgPerTick;
        }
    }
}
