using UnityEngine;

[CreateAssetMenu(menuName = "ProjectileEffects/Piercing")]
public class PiercingProjectile : ProjectileEffectBase
{
    [SerializeField] private int maxPierces = 3;
    public override void OnHit(GameObject projectile, Collider2D hit, int damage)
    {
        var pierceData = projectile.GetComponent<PierceData>();
        if (pierceData == null)
        {
            pierceData = projectile.AddComponent<PierceData>();
            pierceData.Pierces = 0;
        }

        pierceData.Pierces++;

        if (pierceData.Pierces >= maxPierces)
            Destroy(projectile);
    }
}

public class PierceData : MonoBehaviour
{
    public int Pierces;
}
