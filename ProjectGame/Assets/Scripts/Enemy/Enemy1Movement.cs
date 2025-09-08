using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy1Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player; //To assign the player the enemy should follow(Is also able to auto assign with tag)
    public float speed = 1f; // speed that the enemy moves with towards the player
    public float attackRange = 0.5f; //Distance to the enemy before able to attack
    public float attackCooldown = 1f; //Time between attacks
    public float attackDamage = 1f; //Damage the enemy deals to the player with attacks
    private float lastAttackTime;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();



        //If player isn't assigned at the start, assign the player using the player tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > attackRange)
            {
                //If not within attack range moves towards the player
                Vector2 direction = (player.position - transform.position).normalized; //We normalize here to make the speed constant
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

                animator.SetBool("IsMoving", true); //Starts walking animation

                //Flips the enemy sprite so it faces the player
                if (direction.x != 0)
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }
            else
            {
                //Stops moving
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("IsMoving", false); //Stops walking animation

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    
    void AttackPlayer()
    {
        Debug.Log("Enemy attacks player!");
        animator.SetTrigger("Attack"); //Trigger attack animation
        //TO DO: deal damage to the players health based on the enemy attack damage.

    }




}
