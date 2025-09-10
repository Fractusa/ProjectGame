using UnityEngine;

public class Health : MonoBehaviour
{
    public int healAmount = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //PlayerController player = GetComponent<PlayerController>();
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.ChangeHealth(healAmount);
            Destroy(gameObject);
        }
    }
}
