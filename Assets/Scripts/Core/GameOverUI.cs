using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private GameManager gameManager;
    private Damageable playerHealth;

    private void Start()
    {
        gameManager = GameManager.Instance;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();

        // Subscribe to player death event
        playerHealth.OnDeath.AddListener(ShowGameOver);

        // Add button listeners
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        // Hide panel initially
        gameOverPanel.SetActive(false);
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final Score: {gameManager.CurrentScore}";
        gameManager.PauseGame();
    }

    private void RestartGame()
    {
        // Reset game state
        gameManager.ResumeGame();
        gameManager.RestartGame();
        gameOverPanel.SetActive(false);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDeath.RemoveListener(ShowGameOver);
        }
    }
}