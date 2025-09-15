using UnityEngine;

public enum BuffType
{
    Health,
    MovementSpeed,
    MeleeDamage,
    ProjectileDamage,
    AcidDamage,
    FireDamage,
    AttackCooldown
    //ProjectileRange
}
public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int movementSpeed = 10;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private int projectileDamage = 0;
    [SerializeField] private int projectileRange = 0;
    [SerializeField] private int acidDamage = 0;
    [SerializeField] private int fireDamage = 0;
    [SerializeField] private float attackCooldown = 0.5f;

    public int MaxHealth => maxHealth;
    public int MovementSpeed => movementSpeed;
    public int MeleeDamage => meleeDamage;
    public int ProjectileDamage => projectileDamage;
    public int AcidDamage => acidDamage;
    public int FireDamage => fireDamage;
    public int ProjectileRange => projectileRange;
    public float AttackCooldown => attackCooldown;

    public void AddIntBuff(int valueDelta, BuffType type)
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

                // case BuffType.ProjectileRange:
                //     projectileRange += valueDelta;
                //     break;
        }
    }
}
