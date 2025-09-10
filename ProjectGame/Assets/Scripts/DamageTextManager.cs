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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }




    public FloatingDamageText ShowDamage(
        Transform target,
        int amount,
        Color color,
        bool follow = false,
        Vector3? offset = null)
    {

        //Set position for damage numbers
        if (damageTextPrefab == null || canvas == null)
        {
            Debug.LogError("DamageTextManager is missing a prefab");
            return null;
        }




        Vector3 worldPos = target.position + (offset ?? Vector3.zero);
        //convert to local canvas position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        //Instantiate prefab inside the canvas
        FloatingDamageText dmgText = Instantiate(damageTextPrefab, canvas.transform);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            Camera.main,
            out Vector2 localPoint
        );
        (dmgText.transform as RectTransform).localPosition = localPoint;

        //Set text + color
        dmgText.Setup(amount, color, follow, offset, target, worldPos);
        return dmgText;

    }


    
}
