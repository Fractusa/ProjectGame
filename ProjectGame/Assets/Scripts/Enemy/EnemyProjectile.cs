using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private int damage;

    //Called by the enemy to set the projectile's damage
    public void SetDamage(int enemyDamage)
    {
        damage = enemyDamage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Get the player health component and deal the damage
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-damage);
            }

            Destroy(gameObject);//Destroy after it hits
        }
    }

}
