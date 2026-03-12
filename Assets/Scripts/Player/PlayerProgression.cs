using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    public int level = 1;
    public float growthPerLevel = 0.3f;

    public void LevelUp()
    {
        level++;
        transform.localScale += Vector3.one * growthPerLevel;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Consumable>(out var food))
        {
            TryToEat(food);
        }
    }

    void TryToEat(Consumable food)
    {
        if (food.CanEat(level))
        {
            food.GetConsume();
        }
    }
}
