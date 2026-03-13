using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;

    [Header("Slime")]
    [SerializeField] private float moveSpeed;

    private Rigidbody _rb;

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

    void FixedUpdate()
    {
        var move = HandleInput();
        Vector3 finalMove = moveSpeed * Time.deltaTime * move;
        _rb.MovePosition(transform.position + finalMove);
    }

    private Vector3 HandleInput()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = Vector3.ClampMagnitude(new(input.x, 0, input.y), 1f);
        return move;
    }
}
