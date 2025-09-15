using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Stats))]
public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Animator animator;
    public GameObject fireballPrefab;
    public GameObject acidballPrefab;
    public GameObject arrowPrefab;
    public float projectileForce = 300;
    Rigidbody2D rb;
    Vector2 move;
    Vector2 moveDirection = new(1, 0);
    [SerializeField] private Stats playerStats;

    public List<AbilityData> abilities;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<Stats>();
        //data = GetComponent<AbilityData>();
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

        foreach(AbilityData a in abilities)
            a.OnUse(gameObject);
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Launch();
        // }

    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + move * playerStats.MovementSpeed * Time.deltaTime;
        rb.MovePosition(position);
    }

    // void Launch()
    // {
    //     //Finds the mouse position, supplies a z value and then converts from screen space to world space before calculating the shot direction
    //     //The shot direction is then normalized, since we are only interested in the general direction to launch the projectile at
    //     Vector3 mousePos = Input.mousePosition;
    //     mousePos.z = Camera.main.nearClipPlane;
    //     mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    //     Vector3 shootDir = (mousePos - transform.position).normalized;
    //     float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;

    //     if (fireballPrefab != null)
    //     {
    //         GameObject fireballObject = Instantiate(fireballPrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
    //         Projectile proj = fireballObject.GetComponent<Projectile>();
    //         fireballObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    //         proj.Launch(shootDir, projectileForce, playerStats.ProjectileDamage);
    //     }
    //     else if (acidballPrefab != null)
    //     {
    //         //Acidball logic (similar to above)
    //     }
    //     else if (arrowPrefab != null)
    //     {
    //         //Arrow logic (similar to above)
    //     }

    //     //animator.SetTrigger("Launch");
    // }

}
