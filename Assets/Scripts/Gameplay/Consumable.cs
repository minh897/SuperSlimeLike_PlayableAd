using UnityEngine;

public class Consumable : MonoBehaviour
{
    private int level;
    private int expValue;

    public void Init(int levelToSet, float expMultiplier)
    {
        level = levelToSet;
        expValue = (int)(expValue * expMultiplier);
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
