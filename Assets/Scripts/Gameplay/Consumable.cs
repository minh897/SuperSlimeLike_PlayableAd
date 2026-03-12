using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float level = 1;
    public float expValue = 0.2f;

    public float GetExp()
    {
        return expValue;
    }
}
