using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public MeleeWeapon weapon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Swing();
        }
    }



}
