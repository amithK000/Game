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
    }

    public void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1f;
    }

    // Score management
    private int _currentScore;
    public int CurrentScore => _currentScore;

    public void AddScore(int points)
    {
        _currentScore += points;
        // Event system will be implemented here to notify UI
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }
}