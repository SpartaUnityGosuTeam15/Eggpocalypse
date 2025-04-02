using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private InputHandler _input;
    private Rigidbody _rb;
    private Animator _animator;
    [SerializeField] private Transform model;

    [Range(1f, 10f)]
    public float speed = 5f;
    private Vector2 curMovement;

    private void Awake()
    {
        speed += SaveManager.Instance.saveData.GetMoveSpeed();
    }

    private void Start()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<InputHandler>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

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
        if (context.performed) curMovement = context.ReadValue<Vector2>();
        else if (context.canceled) curMovement = Vector2.zero;
    }

    void Move()
    {
        Vector3 dir = Vector3.forward * curMovement.y + Vector3.right * curMovement.x;
        _rb.velocity = dir * speed;

        if (_player.closest != null) Look(_player.closest.transform);
        else Look(dir);

        if(dir != Vector3.zero)
        {
            _animator.SetBool("isMoving", true);
            Vector3 localMove = model.InverseTransformDirection(dir);
            _animator.SetFloat("dX", localMove.x);
            _animator.SetFloat("dY", localMove.z);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

    public void Look(Transform target)
    {
        Vector3 dir = (target.position - transform.position).normalized;
        dir = new Vector3(dir.x, 0, dir.z);
        Look(dir);
    }

    public void Look(Vector3 dir)
    {
        if(dir == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        targetRotation.eulerAngles = targetRotation.eulerAngles + new Vector3(0, 30, 0);
        model.rotation = Quaternion.Slerp(model.rotation, targetRotation, 0.2f);
    }
}
