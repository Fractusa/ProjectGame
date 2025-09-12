using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Scriptable Objects/AbilityData")]
public class AbilityData : ScriptableObject, IAbilityData
{
    [SerializeField] private string abilityName;
    public string Name => abilityName;
    [SerializeField] private AbilityEffectBase[] effects;

    public void OnCleanup(GameObject owner)
    {
        foreach (var e in effects)
            e.OnCleanup(owner);
    }

    public void OnSetup(GameObject owner)
    {
        foreach (var e in effects)
            e.OnSetup(owner);
    }

    public void OnUse(GameObject owner)
    {
        foreach (var e in effects)
            e.OnUse(owner);
    }
}
