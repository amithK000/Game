using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private bool isInvulnerable = false;

    public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    // Events for UI and other systems to subscribe to
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent<float> OnDamageTaken;
    public UnityEvent OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable || IsDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        
        OnDamageTaken?.Invoke(damage);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (IsDead) return;

        float previousHealth = CurrentHealth;
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);

        if (CurrentHealth != previousHealth)
        {
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}