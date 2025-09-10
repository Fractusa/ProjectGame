using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshProUGUI text;


    public float lifetime = 2f;
    public float floatSpeed = 1f;



    private int totalDamage;
    private float lifeTimer;



    private Vector3 initialWorldPosition; // stores thee world position at instantiation
    private Transform followTarget; // The thing we follow (enemy/player)
    private Vector3 offset; //offset above/side of the target
    public bool follow;


    public event Action OnDestroyed; //calls event when the number is destroyed

    public void Setup(
        int amount,
        Color color,
        bool follow = false,
        Vector3? offset = null,
        Transform followTarget = null,
        Vector3? initialWorldPosition = null)
    {
        if (text == null) text = GetComponentInChildren<TextMeshProUGUI>();

        totalDamage = amount;
        text.text = totalDamage.ToString();
        text.color = color;

        this.follow = follow;
        this.followTarget = followTarget;
        this.offset = offset ?? Vector3.zero;

        this.initialWorldPosition = initialWorldPosition ?? Vector3.zero;
        
        lifeTimer = lifetime;

    }

    void Update()
    {
        //Update position to follow our target
        if (follow && followTarget != null)
        {
            //keep DoTs attached to the enemy
            Vector3 screenPos = Camera.main.WorldToScreenPoint(followTarget.position + offset);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                screenPos,
                Camera.main,
                out Vector2 localPoint
            );
            (transform as RectTransform).localPosition = localPoint;
        }
        else
        {
            //normal floating numbers
            //Calculate new world position and convert to local canvas position
            Vector3 newWorldPosition = initialWorldPosition + (Vector3.up * floatSpeed * Time.deltaTime);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(newWorldPosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                screenPos,
                Camera.main,
                out Vector2 localPoint
            );

            (transform as RectTransform).localPosition = localPoint;
            
            //Update world position for the next frame
            initialWorldPosition = newWorldPosition;
        }
        

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
