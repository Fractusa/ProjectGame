using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetTrigger("Die");

        rb.linearVelocity = Vector2.zero;
        this.enabled = false; //disables the script so it won't attack or chase the player while dead

        Destroy(gameObject, 2f); //waits 2 seconds to destroy enemy
    }



}
