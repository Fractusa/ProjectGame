using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI text;


    public float lifetime = 2f;
    public float floatSpeed = 1f;
    public Vector3 floatDirection = Vector3.up;

    private int totalDamage;
    private float lifeTimer;


    public event Action OnDestroyed; //calls event when the number is destroyed

    public void Setup(int amount, Color color)
    {
        if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        totalDamage = amount;
        text.text = totalDamage.ToString();
        text.color = color;
        lifeTimer = lifetime;
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    //Adds damage to the running total
    public void AddDamage(int amount)
    {
        totalDamage += amount;
        if (text != null) text.text = totalDamage.ToString();

        StopAllCoroutines();
        StartCoroutine(PopCoroutine());

        //Reset lifetime so it doesn't vanish while accumulating
        lifeTimer = 5;
    }

    private IEnumerator PopCoroutine()
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * 1.25f;
        float duration = 0.10f;
        float t = 0f;

        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(original, target, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        t = 0f;
        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(target, original, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = original;
    }
}
