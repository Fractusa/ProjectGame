using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //A reference to the one and only instance of this class
    public static PlayerManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            //If no instance of player exists, make this the player instance and keep it.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            //If a player already exists and it's not this instance, delete this copy.
            Destroy(gameObject);
        }
    }
}
