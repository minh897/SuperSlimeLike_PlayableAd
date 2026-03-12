using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int exp;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        Debug.Log("Exp gained: " + exp);

        // if exp reaches a required amount
        // the slime levels up, its size increases
    }
}
