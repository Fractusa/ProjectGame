using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    private int damage;
    Stats stats;
    private Vector2 spawnPos;
    //private bool hasHit = false;
    private ProjectileEffectBase[] effects;

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

    public void Launch(Vector2 direction, float speed, int damageValue, Vector2 playerVelocity)
    {
        damage = damageValue;
        spawnPos = rb.position;

        Vector2 baseVelocity = direction.normalized * speed;
        rb.AddForce(baseVelocity + playerVelocity);
    }

    public void SetEffects(ProjectileEffectBase[] newEffects)
    {
        effects = newEffects;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        { 
            foreach (var effect in effects)
                effect.OnHit(gameObject, other, stats.ProjectileDamage);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
