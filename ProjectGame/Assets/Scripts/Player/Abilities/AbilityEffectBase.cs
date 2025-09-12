using UnityEngine;

[CreateAssetMenu(fileName = "AbilityEffectBase", menuName = "Scriptable Objects/AbilityEffectBase")]
public abstract class AbilityEffectBase : ScriptableObject
{
    public abstract void OnSetup(GameObject owner);
    public abstract void OnUse(GameObject owner);
    public abstract void OnCleanup(GameObject owner);
}
