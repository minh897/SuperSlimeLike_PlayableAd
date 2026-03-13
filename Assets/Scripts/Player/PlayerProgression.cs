using UnityEngine;

public class PlayerProgression : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float growthMultiplier = 0.15f;

    public void LevelUp()
    {
        level++;
        float growthSize = Mathf.Sqrt(level * growthMultiplier);
        transform.localScale += Vector3.one * growthSize;
    }

    public int GetLevel() => level;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Consumable>(out var food))
        {
            if (food.CanEat(level))
            {
                food.GetConsume();
            }
        }
    }

}
