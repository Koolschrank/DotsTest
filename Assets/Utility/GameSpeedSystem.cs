using UnityEngine;

public class GameSpeedSystem : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    void Start()
    {
        Time.timeScale = speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
