using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    private bool isPaused = false;


    void Update()
    {
        if (LevelUpManager.Instance != null && LevelUpManager.Instance.IsLevelingUp())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }



    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // Set time scale to 0 to pause all game actions
        pauseUI.SetActive(true); //activate the pause menu UI
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // Set time scale to 1 to unpause all game actions
        pauseUI.SetActive(false); //Deactivate the pause menu UI
    }
}
