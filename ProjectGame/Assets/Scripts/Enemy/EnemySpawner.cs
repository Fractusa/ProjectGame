using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    [Range(0, 100)] public int weight;
}



public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnData[] enemySpawnData;

    private Transform player;
    private TextMeshProUGUI enemyCountText;

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
    public float healthScalingRate = 10.0f; //Percentage increase per minute
    public float damageScalingRate = 10.0f; //Percentage increase per minute
    public float dropScalingRate = 50.0f; //Percentage increase per minute

    void Awake()
    {
        SetupEnemyParent();
    }
    void Start()
    {
        InitializeSpawner();
    }



    private void SetupEnemyParent()
    {
        // this will create a new GameObject to hold all enemies
        if (enemyParent == null)
        {
            GameObject existingParent = GameObject.Find("Enemies");
            if(existingParent != null)
            {
                enemyParent = existingParent.transform;
                Debug.Log("Enemy parent found and linked");
            }
            else
            {
                GameObject parent = new GameObject("Enemies");
                enemyParent = parent.transform;
                Debug.Log("New enemy parent created");
            }
        }
    }
    private void InitializeSpawner()
    {
        spawnerTurnedOn = true; // start spawner
        spawnTimer = 0f; //Reset timer

        if (player == null)
        {
            // Find the player by tag, assuming your player has the tag "Player"
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player GameObject not found in the scene! Spawner will not work.");
            }
        }
    }


    public void RegisterCountDisplay(TextMeshProUGUI newDisplay)
    {
        enemyCountText = newDisplay;

        if (enemyCountText != null && enemyParent != null)
        {
            enemyCountText.text = $"Enemies: {enemyParent.childCount}"; 
        } 
        else
        {
            // Fallback for safety if enemyParent is somehow missing
            enemyCountText.text = $"Enemies: 0";
        }
    }

    void Update()
    {
        //Check if UI exists before updating
        if(enemyCountText != null && enemyParent != null)
        {
            enemyCountText.text = $"Enemies: {enemyParent.childCount}";
        }
        
        
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
            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        //Calculates the total weight of all enemies in the enemy list
        int totalWeight = 0;
        foreach (var enemy in enemySpawnData)
        {
            totalWeight += enemy.weight;
        }

        int randomNumber = Random.Range(0, totalWeight); //Picks a random number between 0 and the total weight

        GameObject enemyToSpawn = null;
        foreach (var enemy in enemySpawnData)
        {
            if (randomNumber < enemy.weight)
            {
                enemyToSpawn = enemy.enemyPrefab;
                break;
            }
            randomNumber -= enemy.weight;
        }

        if (enemyToSpawn != null)
        {
            Vector2 spawnPos = GetSpawnPosition(player.transform, spawnRange);
            GameObject newEnemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity, enemyParent);

            EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
            EnemyDamage enemyDamage = newEnemy.GetComponent<EnemyDamage>();
            EnemyDrops enemyDrops = newEnemy.GetComponent<EnemyDrops>();

            if (enemyHealth != null && enemyDamage != null)
            {
                // Set the enemy's base stats here, the enemy will scale itself
                enemyHealth.Setup(enemyHealth.maxHealth, healthScalingRate);
                enemyDamage.Setup(enemyDamage.damageAmount, damageScalingRate);
                enemyDrops.Setup(enemyDrops.baseDropScore, dropScalingRate);
            }
        }





    

    }



    private Vector2 GetSpawnPosition(Transform player, float spawnRange)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        return (Vector2)player.position + randomDirection * spawnRange;
    }

}
