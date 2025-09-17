using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack Effect")]
//Style of Attack being used, such as Single Projectile, Burst Fire, Scatter Shot etc.
public abstract class AbilityAttackEffectBase : ScriptableObject
{
    public abstract void OnSetup(GameObject owner);
    public abstract void OnUse(GameObject owner, ProjectileEffectBase[] projectileEffects = null);
    public abstract void OnCleanup(GameObject owner);
}
