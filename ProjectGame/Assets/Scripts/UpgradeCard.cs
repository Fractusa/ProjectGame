using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    private LevelUpManager levelUpManager;

    
    //UI elements for the card
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button selectButton;

    //The bufftype and amount for the card
    private BuffType buffType;
    private int valueDelta;



    public void Setup(LevelUpManager manager, BuffType type, int value, string title, string description)
    {
        levelUpManager = manager;
        buffType = type;
        valueDelta = value;
        titleText.text = title;
        descriptionText.text = description;

    }

    public void OnCardSelected()
    {
        if (levelUpManager != null)
        {
            levelUpManager.SelectUpgrade(buffType, valueDelta);
        }
    }

}
