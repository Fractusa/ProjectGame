using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    public int DamageAmount;
    public DamageType Type;
    public bool IsDoT; //Is it a status effect tick
    public bool CanCrit; //Can the attack crit?
    public List<StatusEffect> Effects; //Effects to apply on hit

    public DamageInfo(int damage, DamageType type = DamageType.Physical, bool isDoT = false, bool canCrit = true)
    {
        DamageAmount = damage;
        Type = type;
        IsDoT = isDoT;
        CanCrit = canCrit;
        Effects = new List<StatusEffect>();
    }



    public static DamageInfo FromEffect(int damage, DamageType type)
    => new DamageInfo(damage, type, isDoT: true, canCrit: false);
}
