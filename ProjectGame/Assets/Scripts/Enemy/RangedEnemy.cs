using System.Runtime.CompilerServices;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private Animator animator;
    private EnemyDamage enemyDamage;


    private enum EnemyState { Moving, Charging, Attacking }
    private EnemyState currentState;

    public bool shouldChargeAttack = false; //Check whether enemy needs to charge their attack
    public bool shouldChargeWhileMoving = false; //Check whether enemy can charge their attack while moving

    public Transform player; //target to attack
    public float moveSpeed = 5f;
    public float attackRange = 10f;
    public float chargeTime = 2f;
    public float projectileSpeed = 10f;
    private float chargeTimer;


    public float attackCooldown = 1f;
    private float lastAttackTime;


    public GameObject projectilePrefab;
    public Transform firePoint; //Decides where the enemy shoots the projectile from


    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyDamage = GetComponent<EnemyDamage>();
        currentState = EnemyState.Moving;


        if (player == null) //If the player isn't manually assigned, try to find them
        {

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
        lastAttackTime = -attackCooldown; //Makes it so the first attack from the enemy doesn't have a cooldown.
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        FlipSprite();

        switch (currentState)
        {
            case EnemyState.Moving: //While the enemy state is set to moving runs the methods to move the enemy
                MoveTowardsPlayer();
                break;

            case EnemyState.Charging:
                ChannelAttack();
                break;

            case EnemyState.Attacking: // We stay in this state until the animation is complete and the attack has occured
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)//Move towards the player
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            animator.SetBool("IsMoving", true);
        }
        else //Stop moving and start attacking
        {
            animator.SetBool("IsMoving", false);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (shouldChargeAttack)
                {
                    //If enemy should charge the attack, start the charging
                    chargeTimer = chargeTime;


                    //If enemy can't move while charging, makes the enemy stand still
                    if (!shouldChargeWhileMoving)
                    {
                        currentState = EnemyState.Charging;
                    }
                }
                else //If the attack doesn't require a charge, just fire it
                {
                    TriggerAttack();
                }
            }
        }
    }

    private void ChannelAttack()
    {
        chargeTimer -= Time.deltaTime;

        if (chargeTimer <= 0 && currentState == EnemyState.Charging)
        {
            //The charge is complete, trigger the attack
            TriggerAttack();
        }

    }

    private void TriggerAttack()
    {
        animator.SetTrigger("Attacking");
        lastAttackTime = Time.time; //record the time the attack happened at

        currentState = EnemyState.Attacking;

        Debug.Log("Triggering attack now!");


    }

    //This will be called once the animation for the attack has been completed
    public void OnAttackAnimationComplete()
    {
        Debug.Log("Attack animation complete!");
        //Instantiate the projectile and shoot it at the player
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            EnemyProjectile projectileScript = newProjectile.GetComponent<EnemyProjectile>();
            if (projectileScript != null && enemyDamage != null)
            {
                projectileScript.SetDamage(enemyDamage.damageAmount);
            }

            Vector2 direction = (player.position - firePoint.position).normalized;
            newProjectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
        }

        //After the attack animation has finished, transition back to the moving state
        currentState = EnemyState.Moving;
    }


         private void FlipSprite()
        {   
            Vector2 direction = (player.position - transform.position).normalized;
            
            if (direction.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }

}

