using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int baseDamage = 10;
    public float attackCooldown = 0.5f;
    public Collider2D hitbox;

    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        if (hitbox != null)
        {
            hitbox.enabled = false; //Disable weapon hitbox at the start
        }
    }

    public void Swing()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            StartCoroutine(DoSwing());
        }
    }
    private System.Collections.IEnumerator DoSwing()
    {
        //enable hitbox for a short duration during swing
        hitbox.enabled = true;
        yield return new WaitForSeconds(0.2f); //Swing active time
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                DamageInfo dmg = new DamageInfo(baseDamage);

                // example for adding effects
                dmg.Effects.Add(new FireEffect(enemy, 5, 1f, 3f));
                dmg.Effects.Add(new BleedEffect(enemy, 2, 0.5f, 5f));

                enemy.TakeDamage(dmg);
            }
        }
    }
}
