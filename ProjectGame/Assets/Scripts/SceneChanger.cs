using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;


    public void PlayAgain()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        if(GameClock.Instance != null)
        {
            GameClock.Instance.ResetClock();
        }
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        if(GameClock.Instance != null)
        {
            GameClock.Instance.ResetClock();
        }
        SceneManager.LoadSceneAsync(0);
    }

}
