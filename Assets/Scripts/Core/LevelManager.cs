using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("LevelManager");
                    _instance = go.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }

    [Header("Level Settings")]
    [SerializeField] private int zombiesToKill = 10;
    [SerializeField] private float levelCompleteDelay = 3f;

    [Header("UI References")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private TextMeshProUGUI levelCompleteText;
    [SerializeField] private Button continueButton;

    private int _zombiesKilled = 0;
    private bool _isLevelComplete = false;
    private bool _isLevelCompleteTriggered = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    // This method is called when a zombie is killed
    // It increments the kill counter and checks if the level is complete
    public void ZombieKilled()
    {
        if (_isLevelComplete) return;

        _zombiesKilled++;
        
        // Check if level is complete
        if (_zombiesKilled >= zombiesToKill)
        {
            LevelComplete();
        }
    }

    private void Start()
    {
        // Initialize UI
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        // Add button listener
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueToNextLevel);
        }
    }

    public void ZombieKilled()
    {
        if (_isLevelComplete) return;

        _zombiesKilled++;
        
        // Check if level is complete
        if (_zombiesKilled >= zombiesToKill)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        _isLevelComplete = true;
        
        // Play level complete sound
        AudioManager.Instance.PlaySound("level_complete");
        
        // Show level complete UI after a short delay
        StartCoroutine(ShowLevelCompleteUI());
    }

    private IEnumerator ShowLevelCompleteUI()
    {
        yield return new WaitForSeconds(levelCompleteDelay);
        
        // Pause the game
        GameManager.Instance.PauseGame();
        
        // Show level complete UI
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            if (levelCompleteText != null)
            {
                levelCompleteText.text = $"Level Complete!\nZombies Killed: {_zombiesKilled}\nScore: {GameManager.Instance.CurrentScore}";
            }
        }
    }

    private void ContinueToNextLevel()
    {
        // Hide level complete UI
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
        
        // Resume game
        GameManager.Instance.ResumeGame();
        
        // Reset level state
        ResetLevel();
        
        // Play background music
        AudioManager.Instance.PlayRandomBackgroundTrack();
    }

    public void ResetLevel()
    {
        _zombiesKilled = 0;
        _isLevelComplete = false;
    }

    public int GetZombiesKilled()
    {
        return _zombiesKilled;
    }

    public int GetZombiesToKill()
    {
        return zombiesToKill;
    }
}