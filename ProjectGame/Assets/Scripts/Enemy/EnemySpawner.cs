using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform player;
    public TextMeshProUGUI enemyCountText;

    //Enemy spawner settings
    private float spawnTimer;
    public float spawnRange;
    public bool spawnerTurnedOn;
    public Transform enemyParent;

    //Enemy spawner scaling
    public float baseSpawnInterval = 2.0f;
    public float minSpawnInterval = 0.1f;
    public float spawnIntervalScaling = 0.5f;

    //Enemy scaling
    public float baseEnemyHealth = 50f;
    public float healthIncreasePerMinute = 10.0f;
    public int baseEnemyDamage = 5;
    public int damageIncreasePerMinute = 1;

    void Start()
    {
        // Optional: If you don't assign an enemyParent in the Inspector,
        // this will create a new GameObject to hold all enemies
        if (enemyParent == null)
        {
            GameObject parent = new GameObject("Enemies");
            enemyParent = parent.transform;
        }
    }
    void Update()
    {
        enemyCountText.text = $"Enemies: {enemyParent.childCount}";
        if (GameClock.Instance == null)
        {
            Debug.LogError("GameClock instance not found, enemy spawn rate won't work");
            return;
        }

        float currentTime = GameClock.Instance.ElapsedTime;
        float currentSpawnInterval = Mathf.Max(
            minSpawnInterval,
            baseSpawnInterval - (spawnIntervalScaling / 60f * currentTime)
        );



        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval && spawnerTurnedOn == true)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetSpawnPosition(player.transform, spawnRange);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyParent);

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
