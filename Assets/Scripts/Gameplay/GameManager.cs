using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ConsumableGroup
{
    public int level;
    public float expMultiplier;
    public List<GameObject> parentObjects;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool GameOver { get; private set; }
    public static bool GameStart { get; private set; }

    [SerializeField] private float gameTime = 30f;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject downloadCTA;

    [SerializeField] private PlayerController player;
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

    void OnDisable()
    {
        Instance = null;
        GameOver = false;
        GameStart = false;
    }

    void Start()
    {
        SetupConsumables();
        UpdateExpLvUI();
    }

    void Update()
    {
        if (GameOver) 
            return;

        if (!GameStart)
            return;

        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            GameOver = true;
            gameTime = 0;
            downloadCTA.SetActive(true);
        }

        UpdateTimerUI();
    }

    public void StartTheGame() => GameStart = true;

    public void UpdateExpLvUI()
    {
        levelText.text = "" + player.Level;
        expText.text = player.Exp + " / " + player.RequireExp;
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(gameTime % 60);
        int milliseconds = Mathf.FloorToInt(gameTime * 100 % 100);
        timerText.text = string.Format("{0:00}.{1:00}", seconds, milliseconds);
    }

    private void SetupConsumables()
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
                    consumable.SetLevel(group.level, group.expMultiplier);
                }
            }
        }

        Debug.Log("Consumables setup completed.");
    }
}
