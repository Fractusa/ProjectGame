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


        selectButton.onClick.AddListener(SelectCard);
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
