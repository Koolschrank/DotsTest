using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;


    InputSystem_Actions inputActions;


    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += move * speed * Time.deltaTime;
    }
}
