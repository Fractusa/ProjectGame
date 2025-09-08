using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 1f;
    public float floatSpeed = 2f;

    public void Setup(string value, Color color)
    {
        text.text = value;
        text.color = color;
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
