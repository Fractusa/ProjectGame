using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public int projectileDestructionRange = 100;
    private int damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.magnitude > projectileDestructionRange)
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
        if(other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                DamageInfo dmg = new DamageInfo(damage, DamageType.Physical, DamageSource.Weapon);

                enemy.TakeDamage(dmg);
            }
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
