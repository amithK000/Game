using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TruckController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;

    [Header("Vehicle Properties")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private Rigidbody _rb;
    private float _currentSpeed;
    private bool _isDead;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (_isDead || GameManager.Instance.IsGamePaused) return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Handle acceleration and deceleration
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, moveSpeed * verticalInput, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }

        // Apply movement
        Vector3 movement = transform.forward * _currentSpeed;
        _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);

        // Handle rotation
        if (Mathf.Abs(_currentSpeed) > 0.1f)
        {
            float turnAmount = horizontalInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
            _rb.MoveRotation(_rb.rotation * turnRotation);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _currentSpeed = 0f;
        // Additional death logic will be implemented here
    }
}