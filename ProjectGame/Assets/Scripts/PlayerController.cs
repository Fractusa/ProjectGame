using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public float movementSpeed;
    public int maxHealth = 5;
    int currentHealth;
    Rigidbody2D rigidbody2D;
    Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Reads the value of Vector2(x, y) and uses transform to add these values to the player character, giving movement
        move = MoveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2D.position + move * movementSpeed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    void ChangeHealth(int amount)
    {
        //Makes sure that health value stays with 0 and max health.
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
