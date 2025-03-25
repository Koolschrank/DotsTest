using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // rotate y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }
}
