using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public GameObject experiencePrefab;
    public GameObject healthPrefab;

    [Range(0f, 1f)] public float healthSpawnChance = 0.1f; //chance for a health orb/potion to spawn

    public int baseDropScore = 1; //Base drop score that decides the enemies base chance for dropping xp/items
    public int currentDropScore;

    public void Setup(int baseScore, float dropScalingRate)
    {

        float currentTime = GameClock.Instance.ElapsedTime;
        float scaledDropScore = baseScore + (baseScore * (dropScalingRate / 100f) / 60f * currentTime);
        currentDropScore = Mathf.RoundToInt(scaledDropScore);
    }

    public void DropLoot()
    {
        DropExperience(); // Always drop xp with the amount based on the drop score

        float healthSpawnValue = Random.value;
        if (healthSpawnValue < healthSpawnChance)
        {
            DropHealth();
        }

    }

    private void DropExperience()
    {
        if (experiencePrefab != null)
        {
            GameObject expInstance = Instantiate(experiencePrefab, transform.position, Quaternion.identity);
            ExpCollectible expCollectible = expInstance.GetComponent<ExpCollectible>();
            if (expCollectible != null)
            {
                expCollectible.experienceAmount = currentDropScore; //Decide the amount of xp drop based on the dropScore
            }
        }
    }

    private void DropHealth()
    {
        if (healthPrefab != null)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
    }

}
