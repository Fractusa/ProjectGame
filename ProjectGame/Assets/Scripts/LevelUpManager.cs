using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    // Make this a singleton so other scripts can easily reference it
    public static LevelUpManager Instance { get; private set; }

    public GameObject levelUpUI;
    public UpgradeCard[] upgradeCards;

    public PauseMenu pauseMenu;

    private bool isLevelingUp = false;

    public bool IsLevelingUp()
    {
        return isLevelingUp;
    }

    public void ShowLevelUpUI()
    {
        isLevelingUp = true;
        pauseMenu.Pause();

        levelUpUI.SetActive(true);

        //Add your own upgradecards to this list
        upgradeCards[0].Setup(this, BuffType.Health, 10, "Health Upgrade", "Increases max health by 10.");
        upgradeCards[1].Setup(this, BuffType.MeleeDamage, 5, "Melee Upgrade", "Increases melee dmg by 5.");
        upgradeCards[2].Setup(this, BuffType.MovementSpeed, 1, "Movement speed upgrade", "Increases movement speed by 1.");


    }

    public void SelectUpgrade(BuffType type, int value)
    {
        ExperienceSystem.Instance.ApplyUpgrade(type, value);
        pauseMenu.Resume();
        levelUpUI.SetActive(false);
        isLevelingUp = false;
    }


}
