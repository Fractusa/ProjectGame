using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage");

        //Show damage numbers above head
        DamageTextManager.Instance.ShowDamage(transform, amount, Color.white);
    }
    private void Die()
    {
        Debug.Log("Player died");
        //To do add death animation/game over/respawn
    }


}
