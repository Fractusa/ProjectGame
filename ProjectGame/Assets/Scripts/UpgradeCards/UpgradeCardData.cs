using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Card", menuName = "Upgrade Card")]
public class UpgradeCardData : ScriptableObject
{
    public enum Rarity{Common, Uncommon, Rare, Epic, Legendary}
    public string title; //Title for the card
    [TextArea(3, 5)] public string description; //Description for the card

    public BuffType buffType; //The type of stat this upgrade affects
    public Rarity rarity; //The rarity of the card

    //The type of value change to use
    public enum ValueType { Flat, Percentage }
    public ValueType valueType;
    public AbilityData targetAbility;

    //The amount to change the stat by
    public float value;
    
}
