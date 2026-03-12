using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;

    [Header("Slime")]
    [SerializeField] private float moveSpeed;

    private Rigidbody _rb;

    private Vector3 _currentDirection;

    private bool _isMoving;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        var move = HandleInput();

        // // Keep the momentum when start moving
        if (move.sqrMagnitude > 0.01f)
        {
            _currentDirection = move;
            if (!_isMoving) 
                _isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if(!_isMoving)
            return;
        
        Vector3 finalMove = moveSpeed * Time.deltaTime * _currentDirection;
        _rb.MovePosition(transform.position + finalMove);
    }

    private Vector3 HandleInput()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = Vector3.ClampMagnitude(new(input.x, 0, input.y), 1f);
        return move;
    }
}
