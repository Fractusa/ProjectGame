using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

[CreateAssetMenu(menuName = "Abilities/Attack Effect/Projectile Spread Shot")]
public class ProjectileSpreadShot : AbilityAttackEffectBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 500f;
    Stats stats;
    AbilityState state;
    Rigidbody2D playerRb;
    PlayerController playerController;
    [SerializeField] private int projectileCount = 2;
    [SerializeField] private float spreadAngle = 30f;

    public override void OnCleanup(GameObject owner)
    {
        throw new System.NotImplementedException();
    }

    public override void OnSetup(GameObject owner)
    {
        stats = owner.GetComponent<Stats>();
        state = owner.GetComponent<AbilityState>();
        playerRb = owner.GetComponent<Rigidbody2D>();
        playerController = owner.GetComponent<PlayerController>();
    }

    public override void OnUse(GameObject owner, AbilityData ability, ProjectileEffectBase[] projectileEffects = null)
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

            int count = projectileCount;
            float step = (count > 1) ? spreadAngle / (count - 1) : 0f;
            float startOffset = -spreadAngle / 2f; // center around baseAngle

            for(int i = 0; i < count; i++)
            {
                // Angle offset for this projectile
                float offset = startOffset + step * i;
                float currentAngle = angle + offset;

                // Convert angle to direction vector
                float rad = currentAngle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
                projectile.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                Projectile proj = projectile.GetComponent<Projectile>();

                if (proj != null)
                {
                    Vector2 playerVelocity = playerController.CurrentVelocity;
                    proj.Launch(dir, projectileSpeed, stats.ProjectileDamage, playerVelocity);

                    if (projectileEffects != null)
                    {
                        proj.SetEffects(projectileEffects);
                    }
                }
            }

            // if (ability.pierces > 0)
            // {
            //     var pierceData = projectile.AddComponent<PierceData>();
            //     pierceData.Pierces = 0;
            //     pierceData.MaxPierces = ability.pierces;
            // }
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
