using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 10;

    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    public void AddExperience(int amount)
    {
        currentXP += amount;
        Debug.Log($"Gained {amount} XP. Total XP: {currentXP}/{xpToNextLevel}");

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        //Increases XP to next level på 10%
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.10f);

        Debug.Log("Levelled up: " + currentLevel);

        //Trigger upgrade UI pick
    }
}
