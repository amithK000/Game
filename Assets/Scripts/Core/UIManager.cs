using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthFillImage;
    [SerializeField] private Color healthFullColor = Color.green;
    [SerializeField] private Color healthLowColor = Color.red;
    [SerializeField] private float lowHealthThreshold = 0.3f;

    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scorePrefix = "Score: ";

    private GameManager gameManager;
    private Damageable playerHealth;

    private void Start()
    {
        gameManager = GameManager.Instance;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();

        // Initialize UI elements
        InitializeHealthUI();
        UpdateScoreUI(gameManager.CurrentScore);

        // Subscribe to events
        playerHealth.OnHealthChanged.AddListener(UpdateHealthUI);
    }

    private void OnEnable()
    {
        if (gameManager != null)
        {
            // Subscribe to score changes when the script is enabled
            gameManager.OnScoreChanged += UpdateScoreUI;
        }
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            // Unsubscribe when the script is disabled
            gameManager.OnScoreChanged -= UpdateScoreUI;
        }
    }

    private void InitializeHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = 100f; // Assuming max health is 100
            UpdateHealthUI(playerHealth.CurrentHealth);
        }
    }

    private void UpdateHealthUI(float currentHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;

            // Update health bar color based on remaining health
            if (healthFillImage != null)
            {
                float healthPercentage = currentHealth / healthSlider.maxValue;
                healthFillImage.color = Color.Lerp(healthLowColor, healthFullColor, 
                    healthPercentage <= lowHealthThreshold ? 0 : (healthPercentage - lowHealthThreshold) / (1 - lowHealthThreshold));
            }
        }
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = scorePrefix + newScore.ToString();
        }
    }
}