using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Stats playerStats;
    public HealthBar healthBar; //Add a public reference to the HealtBar script
    private int currentHealth = 0;
    public int MaxHealth => playerStats.MaxHealth;

    void Start()
    {
        playerStats = GetComponent<Stats>();
        currentHealth = playerStats.MaxHealth;

        //Set up the health bar when player spawns
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHealth);
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    // public void TakeDamage(int amount)
    // {
    //     currentHealth -= amount;
    //     Debug.Log("Player took damage");

    //     //Show damage numbers above head
    //     DamageTextManager.Instance.ShowDamage(transform, amount, Color.white);
    // }
    private void Die()
    {
        Debug.Log("Player died");
        //To do add death animation/game over/respawn
    }

    public void ChangeHealth(int amount)
    {     
        if (amount < 0)
        {
            //Makes sure that health value stays within 0 and max health.
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);

            if(healthBar != null)
            {
                healthBar.SetHealth(currentHealth);
            }

            DamageTextManager.Instance.ShowDamage(transform, amount, Color.red);
            Debug.Log("Player took " + amount + " damage" + MaxHealth);
        }
        else if (amount > 0)
        {
            //Makes sure that health value stays within 0 and max health.
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);

            if(healthBar != null)
            {
                healthBar.SetHealth(currentHealth);
            }

            Debug.Log("Player healed " + amount + " damage");
        }
    }

    

}
