using UnityEngine;

public enum BuffType
{
    Health,
    MovementSpeed,
    MeleeDamage,
    ProjectileDamage,
    AcidDamage,
    FireDamage
}
public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int movementSpeed = 5;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private int projectileDamage = 0;
    [SerializeField] private int projectileRange = 0;
    [SerializeField] private int acidDamage = 0;
    [SerializeField] private int fireDamage = 0;

    public int MaxHealth => maxHealth;
    public int MovementSpeed => movementSpeed;
    public int MeleeDamage => meleeDamage;
    public int ProjectileDamage => projectileDamage;
    public int AcidDamage => acidDamage;
    public int FireDamage => fireDamage;
    public int ProjectileRange => projectileRange;

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
        }
    }
}
