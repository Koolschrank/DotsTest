using UnityEngine;

public class Player : MonoBehaviour
{

    // singelton
    public static Player Instance { get; private set; }


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


