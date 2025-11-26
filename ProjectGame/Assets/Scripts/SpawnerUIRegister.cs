using TMPro;
using UnityEngine;

public class SpawnerUIRegister : MonoBehaviour
{
    public EnemySpawner spawner;

    void Start()
    {
        TextMeshProUGUI display = GetComponent<TextMeshProUGUI>();

        if(spawner != null && display != null)
        {
            spawner.RegisterCountDisplay(display);
        }
        else
        {
            Debug.LogError("SpawnerUIRegistrar is missing the EnemySpawner reference or Text component!");
        }
    }


}
