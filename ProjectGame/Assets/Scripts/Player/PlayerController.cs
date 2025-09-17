using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Stats))]
public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Animator animator;
    Rigidbody2D rb;
    Vector2 move;
    Vector2 moveDirection = new(1, 0);
    [SerializeField] private Stats playerStats;
    public List<AbilityData> abilities;
    Vector2 previousPosition;
    public Vector2 CurrentVelocity { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<Stats>();
        previousPosition = rb.position;

        foreach (var a in abilities)
            a.OnSetup(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Reads the value of Vector2(x, y) and uses transform to add these values to the player character, giving movement
        move = MoveAction.ReadValue<Vector2>();

        //Checks if the users input values are above/below 0 in either x or y, if triggered the Move Direction is set to face the correct direction
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
        }

        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);

        if (move.sqrMagnitude > 0.01f)
        {
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);

        //Iterates over every Ability assigned to the player, and runs the OnUse function, which in turn runs the attached Effects' OnUse function,
        //effectively using the Ability. This allows each Ability to run as many effects as desired.
        foreach (AbilityData a in abilities)
            a.OnUse(gameObject);
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position + move * playerStats.MovementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(position);

        //Calculates the velocity (change in Rigidbody position) of the Player used in Projectile, to take Players velocity into consideration,
        //before launching projectile towards Enemy. This should help with keeping projectiles moving towards enemies, and minimizes enemies dodging.
        CurrentVelocity = (position - previousPosition) / Time.fixedDeltaTime;
        previousPosition = position;
    }
}
