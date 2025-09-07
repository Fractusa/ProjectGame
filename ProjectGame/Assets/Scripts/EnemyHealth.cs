using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    void Start()
    {
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Update()
    {
        //Update all active effects
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].Update(Time.deltaTime);
        }

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

        Destroy(gameObject, 2f); //waits 2 seconds to destroy enemy
    }

    //Adds the effect and checks if the effect is allowed to stack or not
    public void AddEffect(StatusEffect newEffect, bool allowStacks = true)
    {
        //Check for existing effect of same type
        StatusEffect existing = activeEffects.Find(e => e.GetType() == newEffect.GetType());

        if (existing != null)
        {
            existing.Refresh();

            if (allowStacks)
            {
                activeEffects.Add(newEffect);
                newEffect.Apply();
            }
        }

        //no existing effect found, adds the effect
        activeEffects.Add(newEffect);
        newEffect.Apply();


    }

    public void RemoveEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        effect.Remove();
    }

    public bool HasEffect<T>() where T : StatusEffect
    {
        return activeEffects.Exists(e => e is T);
    }



}
