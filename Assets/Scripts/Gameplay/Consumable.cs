using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int level = 1;
    public int expValue = 1;
    public float scaleSize = 0.5f;

    void Start()
    {
        // Scalling food by its level
        transform.localScale = level * scaleSize * Vector3.one;
    }

    public bool CanEat(int playerLevel)
    {
        return playerLevel >= level;
    }

    public void GetConsume()
    {
        GameManager.Instance.AddExp(expValue);
        Destroy(gameObject);
    }
}
