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
}