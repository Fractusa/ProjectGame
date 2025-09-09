using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;
    public FloatingDamageText damageTextPrefab;
    public Canvas canvas;

    //Track active texts per target and it's damagetype
    private Dictionary<(Transform, DamageType), FloatingDamageText> activeTexts = new();

    void Awake()
    {
        Instance = this;
    }




    public FloatingDamageText ShowDamage(Transform target, int amount, Color color)
    {
        if (damageTextPrefab == null)
        {
            Debug.LogError("DamageTextManager: damageTextPrefab is not assigned!");
            return null;
        }
        if (canvas == null)
        {
            Debug.LogError("DamageTextManager: worldCanvas is not assigned!");
            return null;
        }

        //Set position for damage numbers
        Vector3 worldPosition = target.position + Vector3.up * 0.5f;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //Instantiate prefab inside the canvas
        FloatingDamageText dmgText = Instantiate(damageTextPrefab, canvas.transform);

        //Convert screen space to local canvas position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            Camera.main,
            out Vector2 localPoint
        );

        RectTransform rect = dmgText.GetComponent<RectTransform>();
        rect.localPosition = localPoint;

        //Set text + color
        dmgText.Setup(amount, color);

        return dmgText;
    }


    
}
