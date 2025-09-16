using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    private int damage;
    Stats stats;
    private Vector2 spawnPos;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        stats = player.GetComponent<Stats>();
        spawnPos = transform.position;
    }

    void Update()
    {     
        float distanceTraveled = Vector2.Distance(transform.position, spawnPos);

        if (distanceTraveled > stats.ProjectileRange)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force, int damageValue)
    {
        damage = damageValue;
        rb.AddForce(direction.normalized * force);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                DamageInfo dmg = new DamageInfo(damage, DamageType.Physical, DamageSource.Weapon);

                enemy.TakeDamage(dmg);
            }

            hasHit = true;
            Destroy(gameObject);
                
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
