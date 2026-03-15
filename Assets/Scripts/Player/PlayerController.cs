using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int Level => level;
    public int Exp => exp;
    public int RequireExp => requireExp;

    public event Action OnLevelUp;
    
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;

    [Header("Player")]
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private int baseExp = 30;
    [SerializeField] private float expMultiplier = 3f;
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float growthMultiplier = 0.15f;

    private int level = 1;
    private int exp = 0;
    private int requireExp;
    private float currentSpeed;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        requireExp = baseExp;
        currentSpeed = baseMoveSpeed;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Consumable>(out var food))
        {
            if (food.CanBeEaten(level))
                food.GetConsumedBy(this);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.GameOver)
            return;

        var move = HandleInput();

        if (move.sqrMagnitude > 0.01f && !GameManager.GameStart)
            GameManager.Instance.StartTheGame();

        Vector3 finalMove = currentSpeed * Time.deltaTime * move;
        rb.MovePosition(transform.position + finalMove);
    }

    public void GainExp(float amount)
    {
        exp += (int)amount;
        if (exp >= requireExp)
        {
            LevelUp();
            return;
        }
        GameManager.Instance.UpdateExpLvUI();
    }

    private Vector3 HandleInput()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = Vector3.ClampMagnitude(new(input.x, 0, input.y), 1f);
        return move;
    }

    private void LevelUp()
    {
        level++;
        exp = 0;

        // Increase values exponentially
        requireExp = Mathf.RoundToInt(requireExp * Mathf.Pow(expMultiplier, level - 1));
        currentSpeed *= Mathf.Pow(speedMultiplier, level - 1);

        float growthSize = Mathf.Sqrt(level * growthMultiplier);
        transform.localScale += Vector3.one * growthSize;
        GameManager.Instance.UpdateExpLvUI();
        OnLevelUp?.Invoke();
    }

}
