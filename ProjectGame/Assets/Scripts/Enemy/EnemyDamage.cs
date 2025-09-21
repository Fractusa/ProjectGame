using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public float attackCooldown = 1f; //cd between being able to do damage
    private float lastAttackTime = -Mathf.Infinity; // Ensures that the enemy can attack the player the instant he is hit 
    public void Setup(int baseDamage, float damageScalingRate)
    {
        if (GameClock.Instance == null)
        {
            Debug.LogError("GameClock instance not found. Enemy damage will not scale.");
            damageAmount = baseDamage;
        }
        else
        {
            float currentTime = GameClock.Instance.ElapsedTime;
            float scaledDamage = baseDamage + (baseDamage * (damageScalingRate / 100f) / 60f * currentTime);

            damageAmount = Mathf.RoundToInt(scaledDamage);
        }
    }

    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Check if the collision has the player tag
        {
            TryDealDamage(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Check if the collision has the player tag
        {
            TryDealDamage(collision);
        }
    }

    private void TryDealDamage(Collider2D collision)
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //damageAmount is converted to a negative number, to correctly damage the player with ChangeHealth()
                playerHealth.ChangeHealth(-damageAmount);
                lastAttackTime = Time.time; // Reset the cooldown between the attacks
            }
        }
    }
}
