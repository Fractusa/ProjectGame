using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float movementSpeed;
    Animator animator;
    public GameObject fireballPrefab;
    public float projectileForce = 300;

    Rigidbody2D rb;
    Vector2 move;
    Vector2 moveDirection = new(1,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Reads the value of Vector2(x, y) and uses transform to add these values to the player character, giving movement
        move = MoveAction.ReadValue<Vector2>();

        //Checks if the users input values are above 0 in either x or y, if triggered the Move Direction is set to face the correct direction
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
        }

        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);

        if(move.sqrMagnitude > 0.01f)
        {
            animator.SetBool("isMoving", true);
        }
        else
            animator.SetBool("isMoving", false);

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + move * movementSpeed * Time.deltaTime;
        rb.MovePosition(position);
    }

    void Launch()
    {
        GameObject fireballObject = Instantiate(fireballPrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile proj = fireballObject.GetComponent<Projectile>();
        proj.Launch(moveDirection, projectileForce);
        //animator.SetTrigger("Launch");
    }

}
