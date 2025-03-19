using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

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

    // Game state management
    private bool _isGamePaused;
    public bool IsGamePaused => _isGamePaused;

    public void PauseGame()
    {
        _isGamePaused = true;
        Time.timeScale = 0f;
        AudioManager.Instance.PlaySound("pause");
    }

    public void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySound("resume");
    }

    // Score management
    private int _currentScore;
    public int CurrentScore => _currentScore;
    public event System.Action<int> OnScoreChanged;

    public void RestartGame()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke(_currentScore);

        // Find and reset player health
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var damageable = player.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.ResetHealth();
            }
        }

        // Reset any other game state here
        ResumeGame();
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        OnScoreChanged?.Invoke(_currentScore);
        AudioManager.Instance.PlaySound("score");
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }
    
    // Game over handling
    public void GameOver()
    {
        // Find and show the game over UI
        var gameOverUI = FindObjectOfType<GameOverUI>();
        if (gameOverUI != null)
        {
            // The ShowGameOver method will handle pausing and displaying UI
            gameOverUI.SendMessage("ShowGameOver");
        }
        else
        {
            // Fallback if no UI is found
            PauseGame();
            Debug.LogWarning("GameOverUI not found. Game paused.");
        }
        
        // Play game over sound
        AudioManager.Instance.PlaySound("game_over");
    }
}