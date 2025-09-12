using UnityEngine;

public interface IAbilityData
{
    public string Name { get; }
    public void OnSetup(GameObject owner);
    public void OnUse(GameObject owner);
    public void OnCleanup(GameObject owner);
}
