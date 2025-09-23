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
    private ProjectileUpgrade projUpgrade;

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
        stats.ApplyBuff(selectedUpgrade, selectedUpgrade.targetAbility);

        pauseMenu.Resume();
        levelUpUI.SetActive(false);
        isLevelingUp = false;
    }

    private List<UpgradeCardData> GetRandomUpgrades(int count) //Pulls the given amount of cards from the pool
    {
        List<UpgradeCardData> randomUpgrades = new List<UpgradeCardData>();//Creates a list for the chosen cards

        //Creates a temporary list to create a weighted pool of upgrades
        List<UpgradeCardData> weightedPool = new List<UpgradeCardData>();

        foreach (var upgrade in allAvailabledUpgrades)
        {
            int weight = 0;
            switch (upgrade.rarity)
            {
                case UpgradeCardData.Rarity.Common:
                    weight = 100;
                    break;
                case UpgradeCardData.Rarity.Uncommon:
                    weight = 50;
                    break;
                case UpgradeCardData.Rarity.Rare:
                    weight = 30;
                    break;
                case UpgradeCardData.Rarity.Epic:
                    weight = 15;
                    break;
                case UpgradeCardData.Rarity.Legendary:
                    weight = 5;
                    break;
            }

            //Increase the weight of rarer cards based on the players luck stat
            //Each point of luck adds 10 to the weight of Uncommon and above cards
            if (upgrade.rarity > UpgradeCardData.Rarity.Common)
            {
                weight += stats.Luck * 10;
            }

            //Add the upgrade to the weighted pool based on its calculated weight
            for (int i = 0; i < weight; i++)
            {
                weightedPool.Add(upgrade);
            }
        }

        //Ensures that we don't try to pick more cards than are available
        int numToPick = Mathf.Min(count, weightedPool.Count);

        for (int i = 0; i < numToPick; i++)
        {
            int randomIndex = Random.Range(0, weightedPool.Count);
            UpgradeCardData selectedUpgrade = weightedPool[randomIndex];
            randomUpgrades.Add(selectedUpgrade);

            
            weightedPool.RemoveAt(randomIndex); //Removes the picked upgrade from the copy list to prevent duplicates
        }


        return randomUpgrades; //returns the chosen random cards
    }

}
