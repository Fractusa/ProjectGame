using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI valueText;
    public Image cardIcon;
    public Button selectButton;
    public Image rarityBorder;

    private UpgradeCardData cardData;
    private LevelUpManager levelUpManager;


    //This method is called by the UpgradeManager to set up the card
    public void SetupCard(UpgradeCardData data)
    {
        cardData = data;
        levelUpManager = LevelUpManager.Instance;

        //Update the UI elements with the given data
        titleText.text = data.title;
        descriptionText.text = data.description;

        SetRarityColor(data.rarity);//Set the color of the rarity border based on the card's rarity


        //Display the value based on the type(Percentage or flat)
        if (data.valueType == UpgradeCardData.ValueType.Flat)
        {
            valueText.text = $"+{data.value}";
        }
        else
        {
            valueText.text = $"+{data.value}%";
        }

        //add icon based on buff type here
        Debug.Log($"Setting up card for BuffType: {data.buffType}");

        selectButton.onClick.RemoveAllListeners();//Clear old listeners to prevent duplicates
        selectButton.onClick.AddListener(SelectCard);
    }

    private void SetRarityColor(UpgradeCardData.Rarity rarity)//Method to set the border color based on rarity
    {
        switch (rarity)
        {
            case UpgradeCardData.Rarity.Common:
                rarityBorder.color = Color.white;
                break;
            case UpgradeCardData.Rarity.Uncommon:
                rarityBorder.color = Color.green;
                break;
            case UpgradeCardData.Rarity.Rare:
                rarityBorder.color = Color.blue;
                break;
            case UpgradeCardData.Rarity.Epic:
                rarityBorder.color = Color.purple;
                break;
            case UpgradeCardData.Rarity.Legendary:
                rarityBorder.color = new Color(1f, 0.6f, 0f); //Orange color for legendary
                break;
        }
    }

    //This is called by the button when the card is clicked
    public void SelectCard()
    {
        if (levelUpManager != null && cardData != null)
        {
            levelUpManager.SelectUpgrade(cardData);
        }
    }
    
}
