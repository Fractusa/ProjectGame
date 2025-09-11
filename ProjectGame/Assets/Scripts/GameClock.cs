using TMPro;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public static GameClock Instance;

    public TextMeshProUGUI timeText;

    private float elapsedTime = 0f;

    public float ElapsedTime => elapsedTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keeps the clock between scenes
        }

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        DisplayTime(elapsedTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (timeText != null)
        {
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
