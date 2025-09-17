using UnityEngine;

public class ProjectileEffectBase : ScriptableObject
{
    public virtual void OnHit(GameObject projectile, Collider2D hit, int damage)
    {
        
    }
}
