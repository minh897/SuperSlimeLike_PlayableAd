using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    public float growth = 0.2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Consumable>(out var food))
        {
            Eat(food);
        }
    }

    void Eat(Consumable food)
    {
        // Only eat smaller objects (objects that are lower level)
        float mySize = transform.localScale.x;
        if (food.transform.localScale.x < mySize)
        {
            Grow(food.GetExp());
            Destroy(food.gameObject);
        }
    }

    void Grow(float value)
    {
        transform.localScale += Vector3.one * value;
        // Increases slime's exp
    }
}
