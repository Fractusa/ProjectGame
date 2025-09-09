using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int DamageAmount;
    public DamageType Type;
    public DamageSource Source;


    public bool IsDoT; //Is it a status effect tick
    public bool CanCrit; //Can the attack crit?
    public bool IsCrit; //if the attack was a crit


    public List<StatusEffect> Effects; //Effects to apply on hit

    public DamageInfo(int damage, DamageType type, DamageSource source, bool isDoT = false, bool canCrit = true, bool isCrit = false)
    {
        DamageAmount = damage;
        Type = type;
        Source = source;
        IsDoT = isDoT;
        CanCrit = canCrit;
        IsCrit = isCrit;
        Effects = new List<StatusEffect>();
    }



    public static DamageInfo FromEffect(int damage, DamageType type, DamageSource source = DamageSource.StatusEffect)
    {
        return new DamageInfo(damage, type, source, isDoT: true, canCrit: false);
    }
}
