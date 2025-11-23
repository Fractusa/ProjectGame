using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player died");
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        gameOverScreen.transform.SetAsLastSibling();
    }


}
