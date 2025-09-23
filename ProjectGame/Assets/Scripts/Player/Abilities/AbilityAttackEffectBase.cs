using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack Effect")]
//Style of Attack being used, such as Single Projectile, Burst Fire, Scatter Shot etc.
public abstract class AbilityAttackEffectBase : ScriptableObject
{
    //Starting logic that the effect needs to run - get necessary components etc.
    public abstract void OnSetup(GameObject owner);

    //Main logic for the effect, for instance used to find shooting direction and Instantiate projectiles.
    //As PlayerController calls this through AbilityData, can be seen as logic within an Update()
    //projectileEffects is passed through to enable the ability to apply effects directly to Projectiles (pierce, explosion etc.)
    public abstract void OnUse(GameObject owner, AbilityData ability, ProjectileEffectBase[] projectileEffects = null);

    //Ending logic when an effect is removed - currenty unsure about usecase
    public abstract void OnCleanup(GameObject owner);
}
