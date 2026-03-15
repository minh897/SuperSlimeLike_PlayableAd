using UnityEngine;

public class Consumable : MonoBehaviour
{
    private int level;
    private float expValue = 1;

    public void Init(int levelToSet, float expMultiplier)
    {
        level = levelToSet;
        expValue = level * (level + 1) / 2 * expMultiplier;
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
}
