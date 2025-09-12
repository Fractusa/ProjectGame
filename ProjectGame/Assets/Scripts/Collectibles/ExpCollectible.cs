using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    public int experienceAmount = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ExperienceSystem experienceSystem = other.GetComponent<ExperienceSystem>();
            PlayerController player = other.GetComponent<PlayerController>();
            experienceSystem.AddExperience(experienceAmount);

            Debug.Log("Exp: " + experienceSystem.CurrentXP);
            Destroy(gameObject);
        }
    }
}
