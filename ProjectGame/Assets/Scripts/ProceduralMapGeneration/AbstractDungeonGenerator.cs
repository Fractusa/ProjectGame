using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    // Very straightforward, call this method to generate a new dungeon and clear the old one.
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }
    protected abstract void RunProceduralGeneration();
}
