using TMPro;
using UnityEngine;

public class ClockUIRegister : MonoBehaviour
{

    void Start()
    {
        TextMeshProUGUI display = GetComponent<TextMeshProUGUI>();

        if(GameClock.Instance != null && display != null)
        {
            GameClock.Instance.RegisterTimeDisplay(display);
        }
    }


}
