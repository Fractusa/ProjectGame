using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Effect")]
public abstract class AbilityEffectBase : ScriptableObject
{
    public abstract void OnSetup(GameObject owner);
    public abstract void OnUse(GameObject owner, ProjectileEffectBase[] projectileEffects = null);
    public abstract void OnCleanup(GameObject owner);
}
