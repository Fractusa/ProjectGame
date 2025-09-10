using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    public int experienceAmount = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.experience += experienceAmount;

            Debug.Log("Exp: " + player.experience);
            Destroy(gameObject);
        }
    }
}
