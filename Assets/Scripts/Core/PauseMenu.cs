using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        // Initialize button listeners
        resumeButton.onClick.AddListener(OnResumeClick);
        restartButton.onClick.AddListener(OnRestartClick);
        quitButton.onClick.AddListener(OnQuitClick);

        // Initially hide the pause menu
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        // Toggle pause menu when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        if (gameManager.IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        gameManager.PauseGame();
    }

    private void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        gameManager.ResumeGame();
    }

    private void OnResumeClick()
    {
        ResumeGame();
    }

    private void OnRestartClick()
    {
        gameManager.RestartGame();
        ResumeGame();
    }

    private void OnQuitClick()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}