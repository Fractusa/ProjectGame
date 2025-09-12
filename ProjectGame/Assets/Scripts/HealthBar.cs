using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float maxHealth)
    {
        //Set the health bars max value to the characters max health
        slider.maxValue = maxHealth;
        //Set the health bars current value to it's max health
        slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        //Updates the slider's value to the characters current health
        slider.value = health;   
    }
        
    
}
