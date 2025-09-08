using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;
    public FloatingDamageText damageTextPrefab;
    public Canvas worldCanvas;

    void Awake()
    {
        Instance = this;
    }


    public void ShowDamage(Transform target, int amount, Color color)
    {
        //Set position for damage numbers
        Vector3 worldPosition = target.position + Vector3.up * 0.5f;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //Instantiate prefab inside the canvas
        FloatingDamageText dmgText = Instantiate(damageTextPrefab, worldCanvas.transform);

        //Convert screen space to local canvas position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            worldCanvas.transform as RectTransform,
            screenPos,
            Camera.main,
            out Vector2 localPoint
        );

        RectTransform rect = dmgText.GetComponent<RectTransform>();
        rect.localPosition = localPoint;

        rect.pivot = new Vector2(0.5f, 0.5f);

        //Set text + color
        dmgText.Setup(amount.ToString(), color);
    }


    
}
