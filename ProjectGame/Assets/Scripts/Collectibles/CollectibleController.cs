using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField] private GameObject experiencePrefab;
    [SerializeField] private GameObject healthPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        EnemyEvents.OnEnemyDied += SpawnCollectible;
    }

    void OnDisable()
    {
        EnemyEvents.OnEnemyDied -= SpawnCollectible;
    }

    public void SpawnCollectible(Vector2 position, GameObject collectible)
    {
        Instantiate(experiencePrefab, position, Quaternion.identity);
    }
}
