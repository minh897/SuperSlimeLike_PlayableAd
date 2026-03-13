using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ConsumableGroup
{
    public int level;
    public List<GameObject> parentObjects;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    
    public int exp = 0;
    public int expToLevel = 5;

    [SerializeField] private PlayerProgression player;
    [SerializeField] private List<ConsumableGroup> groups;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        levelText.text = "" + player.GetLevel();
        expText.text = exp + "/" + expToLevel;
        SetupConsumables();
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= expToLevel)
        {
            player.LevelUp();
            
            exp = 0;
            levelText.text = "" + player.GetLevel();
        }
        expText.text = exp + "/" + expToLevel;
    }

    [ContextMenu("Setup Consumables")]
    public void SetupConsumables()
    {
        if (groups.Count == 0) return;

        foreach (var group in groups)
        {
            foreach (GameObject parent in group.parentObjects)
            {
                if (parent == null) continue;

                foreach (Transform child in parent.transform)
                {
                    GameObject obj = child.gameObject;

                    // Add BoxCollider if missing
                    if (!obj.TryGetComponent<BoxCollider>(out var col))
                        obj.AddComponent<BoxCollider>();

                    // Add Consumable script
                    Consumable consumable = obj.GetComponent<Consumable>();
                    if (consumable == null)
                        consumable = obj.AddComponent<Consumable>();

                    // Assign level
                    consumable.SetLevel(group.level);
                }
            }
        }

        Debug.Log("Consumables setup completed.");
    }
}
