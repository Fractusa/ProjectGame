using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform player;
    private float spawnTimer;
    public float spawnInterval;
    public float spawnRange;
    public bool spawnerTurnedOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && spawnerTurnedOn == true)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPosition(player.transform, spawnRange);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector2 GetSpawnPosition(Transform player, float spawnRange)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + randomDirection * spawnRange;
    }
}
