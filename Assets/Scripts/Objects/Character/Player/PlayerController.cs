using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputHandler _input;
    private Rigidbody _rb;

    [Range(1f, 10f)]
    public float speed = 5f;
    private Vector2 curMovement;

    private void Start()
    {
        _input = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();

        RemoveInputActionCallbacks();
        AddInputActionCallbacks();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    void AddInputActionCallbacks()
    {
        _input.playerActions.Move.performed += OnMove;
        _input.playerActions.Move.canceled += OnMove;
    }

    void RemoveInputActionCallbacks()
    {
        _input.playerActions.Move.performed -= OnMove;
        _input.playerActions.Move.canceled -= OnMove;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) curMovement = context.ReadValue<Vector2>();
        else if(context.canceled) curMovement = Vector2.zero;
    }

    void Move()
    {
        Vector3 dir = Vector3.forward * curMovement.y + Vector3.right * curMovement.x;
        _rb.velocity = dir * speed;
    }
}
