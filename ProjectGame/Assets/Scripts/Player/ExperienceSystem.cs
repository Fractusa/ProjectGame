using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance { get; private set; }
    public LevelUpManager levelUpManager;
    public Stats stats;

    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 10;

    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    private void Awake()
    {
        // This is the core of the singleton pattern.
        // It ensures there is only one instance of this class at any time.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
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
        //Increases XP to next level by 10%
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.10f);

        Debug.Log("Levelled up: " + currentLevel);

        //Trigger upgrade UI pick
        levelUpManager.ShowLevelUpUI();

    }
}
