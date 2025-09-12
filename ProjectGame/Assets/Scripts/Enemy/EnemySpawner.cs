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

    public float baseEnemyHealth = 50f;
    public float healthIncreasePerMinute = 10.0f;
    public int baseEnemyDamage = 5;
    public int damageIncreasePerMinute = 1;

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
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
        EnemyDamage enemyDamage = newEnemy.GetComponent<EnemyDamage>();


        if (enemyHealth != null && enemyDamage != null)
        {
            // Set the enemy's base stats here, the enemy will scale itself
            enemyHealth.Setup(baseEnemyHealth, healthIncreasePerMinute);
            enemyDamage.Setup(baseEnemyDamage, damageIncreasePerMinute);
        }
    }



    private Vector2 GetSpawnPosition(Transform player, float spawnRange)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + randomDirection * spawnRange;
    }

}
