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


    public void ShowDamage(Vector3 worldPosition, int amount, Color color)
    {
        //Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        //Instantiate prefab inside the canvas
        FloatingDamageText dmgText = Instantiate(damageTextPrefab, worldCanvas.transform);

        //Place the damage numbers at the converted screen position
        dmgText.transform.position = screenPos;

        //Set text + color
        dmgText.Setup(amount.ToString(), color);

        Debug.Log("Damage text screen position: " + dmgText.transform.position);
    }


    
}
