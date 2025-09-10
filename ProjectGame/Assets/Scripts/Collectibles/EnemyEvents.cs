using System;
using UnityEngine;

public static class EnemyEvents
{
    public static event Action<Vector2, GameObject> OnEnemyDied;

    public static void EnemyDied(Vector2 position, GameObject collectiblePrefab)
    {
        OnEnemyDied?.Invoke(position, collectiblePrefab);
    }

}
