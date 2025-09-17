using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    // Make this a singleton so other scripts can easily reference it
    public static LevelUpManager Instance { get; private set; }

    public GameObject levelUpUI;
    public Transform cardSpawnParent;


    public List<UpgradeCardData> allAvailabledUpgrades; //All upgradecards in the pool
    public GameObject upgradeCardPrefab;

    public PauseMenu pauseMenu;
    public Stats stats;

    private bool isLevelingUp = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool IsLevelingUp()
    {
        return isLevelingUp;
    }


    public void ShowLevelUpUI() //Called by the ExperienceSystem script when the player levels up
    {
        isLevelingUp = true;
        pauseMenu.Pause();
        levelUpUI.SetActive(true);

        //Clear previous cards
        foreach (Transform child in cardSpawnParent)
        {
            Destroy(child.gameObject);
        }

        List<UpgradeCardData> upgradesToShow = GetRandomUpgrades(3); //Show 3 random cards from the pool

        foreach (var upgradeData in upgradesToShow)
        {
            GameObject newCardGameObject = Instantiate(upgradeCardPrefab, cardSpawnParent);
            UpgradeCardUI newCardUI = newCardGameObject.GetComponent<UpgradeCardUI>();

            if (newCardUI != null)
            {
                newCardUI.SetupCard(upgradeData);
            }
        }

    }

    //Called by the UpgradeCardUI when a card is selected
    public void SelectUpgrade(UpgradeCardData selectedUpgrade)
    {
        //Apply the buff to the players stats
        stats.ApplyBuff(selectedUpgrade);
        
        pauseMenu.Resume();
        levelUpUI.SetActive(false);
        isLevelingUp = false;
    }

    private List<UpgradeCardData> GetRandomUpgrades(int count) //Pulls the given amount of cards from the pool
    {
        List<UpgradeCardData> randomUpgrades = new List<UpgradeCardData>();//Creates a list for the chosen cards

        //Creates a copy of our full list of available cards, to not modify the original list when removing cards to prevent duplicates
        List<UpgradeCardData> upgradesToPickFrom = new List<UpgradeCardData>(allAvailabledUpgrades);

        //Ensures that we don't try to pick more cards than are available
        int numToPick = Mathf.Min(count, upgradesToPickFrom.Count);

        for (int i = 0; i < numToPick; i++)
        {
            int randomIndex = Random.Range(0, upgradesToPickFrom.Count);
            randomUpgrades.Add(upgradesToPickFrom[randomIndex]);
            upgradesToPickFrom.RemoveAt(randomIndex); //Removes the picked upgrade from the copy list to prevent duplicates
        }


        return randomUpgrades; //returns the chosen random cards
    }

}
