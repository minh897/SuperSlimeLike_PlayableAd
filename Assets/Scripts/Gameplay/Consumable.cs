using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private int level = 1;

    private int expValue;

    void Start()
    {
        expValue = level * (level + 1) / 2;
    }

    public bool CanBeEaten(int playerLevel)
    {
        return playerLevel >= level;
    }

    public void GetConsumedBy(PlayerController player)
    {
        player.GainExp(expValue);
        gameObject.SetActive(false);
    }

    public void SetLevel(int levelToSet, float expMultiplier)
    {
        level = levelToSet;
        expValue = (int)(expValue * expMultiplier);
    }
}
