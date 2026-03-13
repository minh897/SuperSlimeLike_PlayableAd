using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int expValue = 1;

    public int ExpValue => level * (level + 1) / 2;

    public bool CanEat(int playerLevel)
    {
        return playerLevel >= level;
    }

    public void GetConsume()
    {
        GameManager.Instance.AddExp(ExpValue);
        gameObject.SetActive(false);
    }

    public void SetLevel(int levelToSet)
    {
        level = levelToSet;
    }
}
