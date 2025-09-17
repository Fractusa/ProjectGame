using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Abilities/Effect/Projectile Cast")]
public class ProjectileCast : AbilityEffectBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 500f;
    Stats stats;
    AbilityState state;
    Rigidbody2D playerRb;

    public override void OnCleanup(GameObject owner)
    {
        throw new System.NotImplementedException();       
    }

    public override void OnSetup(GameObject owner)
    {
        stats = owner.GetComponent<Stats>();
        state = owner.GetComponent<AbilityState>();
        playerRb = owner.GetComponent<Rigidbody2D>();
    }

    public override void OnUse(GameObject owner, ProjectileEffectBase[] projectileEffects = null)
    {
        GameObject closestEnemy = FindClosestEnemy(owner.transform.position);
        if (closestEnemy == null) return;
        
        //Finds the AbilityState component from the owner, if it doesn't exist assigns it to the owner.
        if (state == null) state = owner.AddComponent<AbilityState>();

        //Find shoot direction and calculate angle
        Vector2 shootDir = (closestEnemy.transform.position - owner.transform.position).normalized;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

        //Spawns the projectile positioned closer towards the top middle of caster
        Vector2 spawnPos = playerRb.position + Vector2.up * 0.5f;

        if (Time.time >= state.LastAttackTime + stats.AttackCooldown)
        {
            state.LastAttackTime = Time.time;
            GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            //Rotate projectile to face correct direction
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            Projectile proj = projectile.GetComponent<Projectile>();

            if (proj != null)
                proj.Launch(shootDir, projectileSpeed, stats.ProjectileDamage);

            if (projectileEffects != null)
            {
                proj.SetEffects(projectileEffects);
            }
        }                    
    }

    private GameObject FindClosestEnemy(Vector2 origin)
    {
        //Finds enemies tagged "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            return null;

        foreach (var e in enemies)
            if (Vector2.Distance(origin, e.transform.position) < stats.ProjectileRange)
            {
                return enemies
                    .OrderBy(e => Vector2.Distance(origin, e.transform.position))
                    .FirstOrDefault();
            }

        return null;
    }
}
