using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI titleText;

    private void Start()
    {
        // Initialize button listeners
        startGameButton.onClick.AddListener(OnStartGameClick);
        controlsButton.onClick.AddListener(OnControlsClick);
        quitButton.onClick.AddListener(OnQuitClick);
        backButton.onClick.AddListener(OnBackClick);

        // Initially hide the controls panel
        controlsPanel.SetActive(false);

        // Set the game title
        titleText.text = "Truck vs. Zombies:\nArlington Rampage";
    }

    private void OnStartGameClick()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

    private void OnControlsClick()
    {
        // Show the controls panel
        controlsPanel.SetActive(true);
    }

    private void OnBackClick()
    {
        // Hide the controls panel
        controlsPanel.SetActive(false);
    }

    private void OnQuitClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}