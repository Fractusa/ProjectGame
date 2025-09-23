using UnityEngine;
using System.Linq;

public class ProjectileUpgrade : MonoBehaviour
{
    public PlayerController player;

    public void PierceUpgrade()
    {
        foreach (var ability in player.abilities)
        {
            if (ability.ProjectileEffects.Any(e => e is PiercingProjectile))
            {
                ability.pierces++;
                return;
            }
        }
    }
}
