using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BuffType
{
    //Player Stats
    Health,
    MovementSpeed,
    MeleeDamage,
    ProjectileDamage,
    AcidDamage,
    FireDamage,
    AttackCooldown,
    ProjectileRange,
    Luck,

    //Ability Stats
    Pierces
}
public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int movementSpeed = 10;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private int projectileDamage = 0;
    [SerializeField] private int projectileRange = 100;
    [SerializeField] private int acidDamage = 0;
    [SerializeField] private int fireDamage = 0;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int luck = 0;

    public int MaxHealth => maxHealth;
    public int MovementSpeed => movementSpeed;
    public int MeleeDamage => meleeDamage;
    public int ProjectileDamage => projectileDamage;
    public int AcidDamage => acidDamage;
    public int FireDamage => fireDamage;
    public int ProjectileRange => projectileRange;
    public float AttackCooldown => attackCooldown;
    public int Luck => luck;

    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // This method applies a buff based on the selected upgrade
    public void ApplyBuff(UpgradeCardData upgrade, AbilityData targetAbility)
    {
        switch (upgrade.buffType)
        {
            //Player stats
            case BuffType.Health:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    maxHealth += Mathf.RoundToInt(upgrade.value);
                else
                    maxHealth += Mathf.RoundToInt(maxHealth * (upgrade.value / 100f));
                break;
            case BuffType.MovementSpeed:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    movementSpeed += Mathf.RoundToInt(upgrade.value);
                else
                    movementSpeed += Mathf.RoundToInt(movementSpeed * (upgrade.value / 100f));
                break;
            case BuffType.MeleeDamage:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    meleeDamage += Mathf.RoundToInt(upgrade.value);
                else
                    meleeDamage += Mathf.RoundToInt(meleeDamage * (upgrade.value / 100f));
                break;
            case BuffType.ProjectileDamage:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    projectileDamage += Mathf.RoundToInt(upgrade.value);
                else
                    projectileDamage += Mathf.RoundToInt(projectileDamage * (upgrade.value / 100f));
                break;
            case BuffType.AcidDamage:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    acidDamage += Mathf.RoundToInt(upgrade.value);
                else
                    acidDamage += Mathf.RoundToInt(acidDamage * (upgrade.value / 100f));
                break;
            case BuffType.FireDamage:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    fireDamage += Mathf.RoundToInt(upgrade.value);
                else
                    fireDamage += Mathf.RoundToInt(fireDamage * (upgrade.value / 100f));
                break;
            case BuffType.ProjectileRange:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    projectileRange += Mathf.RoundToInt(upgrade.value);
                else
                    projectileRange += Mathf.RoundToInt(projectileRange * (upgrade.value / 100f));
                break;
            case BuffType.AttackCooldown:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    attackCooldown += upgrade.value;
                else
                    attackCooldown += attackCooldown * (upgrade.value / 100f);
                break;
            case BuffType.Luck:
                if (upgrade.valueType == UpgradeCardData.ValueType.Flat)
                    luck += Mathf.RoundToInt(upgrade.value);
                else
                    luck += Mathf.RoundToInt(luck * (upgrade.value / 100f));
                break;

            //Ability stats
            case BuffType.Pierces:
                if (targetAbility != null )
                {
                    //Find the player's instance of the ability
                    AbilityData playerAbility = player.abilities.FirstOrDefault(a => a.Name == targetAbility.Name);

                    if(playerAbility != null && playerAbility.ProjectileEffects.Any(e => e is PiercingProjectile))                
                        playerAbility.pierces += Mathf.RoundToInt(upgrade.value);

                    Debug.Log($"{targetAbility.Name} pierces = {targetAbility.pierces}");              
                }
                
                break;

        }
    }

    /*public void AddIntBuff(int valueDelta, BuffType type)
    {
        switch (type)
        {
            case BuffType.Health:
                maxHealth += valueDelta;
                break;
            case BuffType.MovementSpeed:
                movementSpeed += valueDelta;
                break;
            case BuffType.MeleeDamage:
                meleeDamage += valueDelta;
                break;
            case BuffType.ProjectileDamage:
                projectileDamage += valueDelta;
                break;
            case BuffType.AcidDamage:
                acidDamage += valueDelta;
                break;
            case BuffType.FireDamage:
                fireDamage += valueDelta;
                break;
            //case BuffType.AttackCooldown:

            case BuffType.ProjectileRange:
                projectileRange += valueDelta;
                break;
        }
    }*/
}
