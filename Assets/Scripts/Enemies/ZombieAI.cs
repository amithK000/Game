using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Damageable))]
public class ZombieAI : MonoBehaviour
{
    [Header("Zombie Settings")]
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;

    private NavMeshAgent _agent;
    private Damageable _damageable;
    private Transform _target;
    private float _nextAttackTime;
    private bool _isDead;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _damageable = GetComponent<Damageable>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        // Subscribe to death event
        _damageable.OnDeath.AddListener(HandleDeath);
    }

    private void Update()
    {
        if (_isDead || GameManager.Instance.IsGamePaused) return;

        float distanceToTarget = Vector3.Distance(transform.position, _target.position);

        if (distanceToTarget <= detectionRange)
        {
            if (distanceToTarget <= attackRange)
            {
                AttackTarget();
            }
            else
            {
                ChaseTarget();
            }
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    private void ChaseTarget()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_target.position);
    }

    private void AttackTarget()
    {
        _agent.isStopped = true;
        transform.LookAt(_target);

        if (Time.time >= _nextAttackTime)
        {
            // Try to get the Damageable component from the target
            if (_target.TryGetComponent<Damageable>(out Damageable targetDamageable))
            {
                targetDamageable.TakeDamage(attackDamage);
                _nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void HandleDeath()
    {
        _isDead = true;
        _agent.isStopped = true;
        
        // Notify LevelManager about zombie kill
        LevelManager.Instance.ZombieKilled();
        
        // Play death sound
        AudioManager.Instance.PlaySound("zombie_death", transform.position);
        
        // Return to object pool after delay for death animation
        StartCoroutine(ReturnToPoolAfterDelay(3f));
    }
    
    private System.Collections.IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Reset zombie state before returning to pool
        _isDead = false;
        
        // Return to object pool
        ObjectPool.Instance.ReturnToPool(gameObject, ZombieSpawner.Instance.ZombiePrefab);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize detection and attack ranges in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}