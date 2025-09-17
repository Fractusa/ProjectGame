using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileEffects/Damage")]
public class ProjectileDamage : ProjectileEffectBase
{
    public override void OnHit(GameObject projectile, Collider2D hit, int damage)
    {
        if (hit.CompareTag("Enemy") && hit.TryGetComponent<EnemyHealth>(out var enemy))
        {
            DamageInfo dmg = new DamageInfo(damage, DamageType.Physical, DamageSource.Weapon);
            enemy.TakeDamage(dmg);
        }
    }
}
