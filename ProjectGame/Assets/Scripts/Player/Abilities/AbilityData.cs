using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class AbilityData : ScriptableObject, IAbilityData
{
    [SerializeField] private string abilityName;
    public string Name => abilityName;
    [SerializeField] private AbilityAttackEffectBase[] effects;
    public AbilityAttackEffectBase[] Effects => effects;
    [SerializeField] private ProjectileEffectBase[] projectileEffects;
    public ProjectileEffectBase[] ProjectileEffects => projectileEffects;

    public int pierces = 1;
    public int explosionRadius = 0;

    //Logic to be run whenever an effect is removed from the ability
    public void OnCleanup(GameObject owner)
    {
        foreach (var e in effects)
            e.OnCleanup(owner);
    }

    //Logic to be run whenever an effect is first added to the ability
    public void OnSetup(GameObject owner)
    {
        foreach (var e in effects)
            e.OnSetup(owner);
    }

    //Logic to be run when the ability is used - done automatically via PlayerController in Update()
    //projectileEffects are passed through here, to enable applying effects to Projectile object rather than Player object
    public void OnUse(GameObject owner)
    {
        foreach (var e in effects)
            e.OnUse(owner, this, projectileEffects);
    }
}
