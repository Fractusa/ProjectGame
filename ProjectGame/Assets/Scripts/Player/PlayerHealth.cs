using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(Stats))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Stats playerStats;
    public HealthBar healthBar; //Add a public reference to the HealtBar script
    private int currentHealth = 0;
    public int MaxHealth => playerStats.MaxHealth;
    private bool isDead = false;

    public static event Action OnPlayerDied; //Event to trigger once the player dies, EventSystem subscribes to this

    void Start()
    {
        isDead = false;
        playerStats = GetComponent<Stats>();
        currentHealth = playerStats.MaxHealth;

        //Set up the health bar when player spawns
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHealth);
        }
    }

    // void Update()
    // {
    //     if (currentHealth <= 0)
    //     {
    //         Die();

    //     }
    // }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died");
        OnPlayerDied?.Invoke();

    }

    public void ChangeHealth(int amount)
    {
        if (isDead) return;

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
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    

}
