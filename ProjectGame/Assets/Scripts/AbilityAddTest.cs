using UnityEngine;

public class AbilityAddTest : MonoBehaviour
{
    [SerializeField] private AbilityData ability;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.AddAbility(ability);

            Destroy(gameObject);
        }
    }
}
