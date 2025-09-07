using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private List<StatusEffect> activeEffects = new List<StatusEffect>(); //Existing effects

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
            if (activeEffects[i].IsFinished)//Check if any effects are finished and remove them if they are
            {
                activeEffects[i].Remove();
                activeEffects.RemoveAt(i);
            }
        }

    }

    public void TakeDamage(DamageInfo info)
    {
        int finalDamage = info.DamageAmount;

        //add multipliers for dmg here, for example extra dmg against burning enemies.





        currentHealth -= Mathf.Max(0, finalDamage);
        Debug.Log("Enemy took " + finalDamage + " damage, Current HP: " + currentHealth);
        animator.SetTrigger("Hurt");

        if (info.Effects != null && info.Effects.Count > 0)
        {
            foreach (var effect in info.Effects)
            {
                AddEffect(effect, allowStacks: true, refreshIfExists: true);
            }
        }
        

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
    public void AddEffect(StatusEffect newEffect, bool allowStacks = true,  bool refreshIfExists = true)
    {
        var effectType = newEffect.GetType();

        //Count how many stacks are currently on the target
        int activeCount = activeEffects.Count(e => e.GetType() == effectType);

        if (activeCount >= newEffect.MaxStacks)
        {
            foreach (var effect in activeEffects.Where(e => e.GetType() == effectType))
            {
                effect.Refresh();
            }
            //trigger max stack effect
            newEffect.OnMaxStacksReached();
            return;
        }

        //Check for existing effect of same type
        if (refreshIfExists && activeCount > 0)
        {
            foreach (var effect in activeEffects.Where(e => e.GetType() == effectType))
            {
                effect.Refresh();
            }
        }

        //no existing effect found, adds the effect
        if (allowStacks || activeCount == 0)
        {
            activeEffects.Add(newEffect);
            newEffect.Apply();
        }
    }

    public void RemoveEffect(StatusEffect effect)
    {
        if(activeEffects.Remove(effect))
        effect.Remove();
    }

    public bool HasEffect<T>() where T : StatusEffect
    => activeEffects.Exists(e => e is T);



}
