using UnityEngine;

public static class DamageColors
{
    public static Color GetColor(DamageType type)
    {
        return type switch
        {
            DamageType.Physical => Color.white,
            DamageType.Fire => new Color(1f, 0.3f, 0.1f), // orange
            DamageType.Bleed => new Color(0.8f, 0f, 0f), // red
            DamageType.Poison => new Color(0.1f, 0.8f, 0.1f), //green
            DamageType.Ice => Color.cyan,
            DamageType.Lightning => Color.yellow,
            _                   => Color.white,

        };
    }
}
