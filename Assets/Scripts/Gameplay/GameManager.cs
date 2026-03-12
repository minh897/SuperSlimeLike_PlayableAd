using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    
    public int exp = 0;
    public int expToLevel = 5;

    [SerializeField] private PlayerProgression player;

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
        levelText.text = "" + player.level;
        expText.text = exp + "/" + expToLevel;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= expToLevel)
        {
            player.LevelUp();
            
            exp = 0;
            levelText.text = "" + player.level;
        }
        expText.text = exp + "/" + expToLevel;
    }
}
