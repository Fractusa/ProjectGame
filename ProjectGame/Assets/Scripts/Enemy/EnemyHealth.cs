using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] float healthSpawnChance = 0.1f;
    private Animator animator;
    public HealthBar healthBar; //Add a public reference to the HealtBar script
    [SerializeField] private GameObject experiencePrefab;
    [SerializeField] private GameObject healthPrefab;

    private Rigidbody2D rb;
    private List<StatusEffect> activeEffects = new List<StatusEffect>(); //Existing effects
    private EnemyDrops enemyDrops;

    private Dictionary<DamageType, FloatingDamageText> activeDotTexts = new Dictionary<DamageType, FloatingDamageText>();

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyDrops = GetComponent<EnemyDrops>();

        //Set up the health bar when enemy spawns
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void Setup(float baseHealth, float healthScalingRate)
    {
        if (GameClock.Instance == null)
        {
            Debug.LogError("GameClock instance not found. Enemy health will not scale.");
            maxHealth = (int)baseHealth;
        }
        else
        {
            float currentTime = GameClock.Instance.ElapsedTime;
            float scaledHealth = baseHealth + (baseHealth * (healthScalingRate / 100f) / 60f * currentTime);
            maxHealth = Mathf.RoundToInt(scaledHealth);
        }

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

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void TakeDamage(DamageInfo info)
    {
        int finalDamage = info.DamageAmount;
        currentHealth -= Mathf.Max(0, finalDamage);


        if(healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        Debug.Log($"Enemy took {finalDamage} {info.Type} damage, HP: {currentHealth}");

        //Spawn damage numbers
        Color dmgColor = DamageColors.GetColor(info.Type);


        if (info.IsDoT)
        {
            ShowAccumulatedDot(info, dmgColor);
        }
        else
        {
            //normal damage doesn't follow the target
            DamageTextManager.Instance.ShowDamage(
                transform,
                finalDamage,
                dmgColor,
                info.IsDoT,
                offset: Vector3.up);
        }



        animator.SetTrigger("Hurt");

        if (info.Effects != null && info.Effects.Count > 0)
        {
            foreach (var effect in info.Effects)
            {
                AddEffect(effect, allowStacks: true, refreshIfExists: true);
            }
        }

    }

    public void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetTrigger("Die");

        foreach (var entry in activeDotTexts)
        {
            if (entry.Value != null)
            {
                Destroy(entry.Value.gameObject);
            }
        }
        activeDotTexts.Clear();

        /*float healthSpawnValue = Random.value;
        if (healthSpawnValue < healthSpawnChance)
            EnemyEvents.EnemyDied(rb.position, healthPrefab);
        EnemyEvents.EnemyDied(rb.position, experiencePrefab);*/

        enemyDrops.DropLoot();




        Destroy(gameObject); //destroys enemy
    }

    public void ShowAccumulatedDot(DamageInfo info, Color color)
    {
        if (activeDotTexts.TryGetValue(info.Type, out FloatingDamageText dotText))
        {
            if (dotText != null)
            {
                //Adds to existing number
                dotText.AddDamage(info.DamageAmount);
            }
            else
            {
                //Cleans up number and creates new dot number
                activeDotTexts.Remove(info.Type);
                CreateNewDot(info, color);
            }
        }
        else
        {
            //First time dot is applied(Creates new number)
            CreateNewDot(info, color);
        }
    }

    private void CreateNewDot(DamageInfo info, Color color)
    {
        int index = activeDotTexts.Count;

        //Set vertical offset based on amount of active dots, so dot damage numbers don't stack ontop of each other
        Vector3 offset = Vector3.right * 0.5f + (Vector3.down * (0.5f * index));

        FloatingDamageText dotText = DamageTextManager.Instance.ShowDamage(
        transform,
        info.DamageAmount,
        color,
        follow: true, //Stick to enemy
        offset: offset  //offsets numbers to prevent stacking
        );

        if (dotText == null) return;


        activeDotTexts[info.Type] = dotText;

        //When the floating texts destroys itself, remove from dictionary
        dotText.OnDestroyed += () =>
        {
            //Remove if it's still mapped to this instance
            if (activeDotTexts.TryGetValue(info.Type, out var current) && current == dotText)
            {
                activeDotTexts.Remove(info.Type);
            }
        };

    }


    //Adds the effect and checks if the effect is allowed to stack or not
    public void AddEffect(StatusEffect newEffect, bool allowStacks = true, bool refreshIfExists = true)
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
        if (activeEffects.Remove(effect))
            effect.Remove();
    }

    public bool HasEffect<T>() where T : StatusEffect
    => activeEffects.Exists(e => e is T);

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }



}
