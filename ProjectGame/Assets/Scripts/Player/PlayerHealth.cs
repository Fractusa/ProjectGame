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
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            DamageTextManager.Instance.ShowDamage(transform, amount, Color.white);
            Debug.Log("Player took " + amount + " damage");
        }
        else if (amount > 0)
        {
            //Makes sure that health value stays within 0 and max health.
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

            Debug.Log("Player healed " + amount + " damage");
        }
    }

    

}
