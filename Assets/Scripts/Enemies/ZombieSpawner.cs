using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float minSpawnDistance = 20f;
    [SerializeField] private float maxSpawnDistance = 50f;
    [SerializeField] private int zombiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 30f;
    [SerializeField] private float difficultyScalingFactor = 1.2f;

    [Header("Spawn Points")]
    [SerializeField] private bool useRandomSpawnPoints = true;
    [SerializeField] private Transform[] spawnPoints;

    private Transform _playerTransform;
    private float _nextWaveTime;
    private int _currentWave = 0;
    private List<GameObject> _activeZombies = new List<GameObject>();

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _nextWaveTime = Time.time + timeBetweenWaves;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePaused) return;

        if (Time.time >= _nextWaveTime)
        {
            SpawnWave();
        }

        // Clean up destroyed zombies from the list
        _activeZombies.RemoveAll(zombie => zombie == null);
    }

    private void SpawnWave()
    {
        _currentWave++;
        int zombiesToSpawn = Mathf.RoundToInt(zombiesPerWave * Mathf.Pow(difficultyScalingFactor, _currentWave - 1));

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            SpawnZombie();
        }

        _nextWaveTime = Time.time + timeBetweenWaves;
    }

    private void SpawnZombie()
    {
        Vector3 spawnPosition;

        if (!useRandomSpawnPoints || spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPosition = GetRandomSpawnPosition();
        }
        else
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPosition = spawnPoint.position;
        }

        GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        _activeZombies.Add(zombie);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

        Vector3 spawnPosition = _playerTransform.position + direction * distance;
        spawnPosition.y = _playerTransform.position.y; // Ensure same height as player

        return spawnPosition;
    }

    public int GetActiveZombieCount()
    {
        return _activeZombies.Count;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying && transform.position != null)
        {
            // Visualize spawn range in editor
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minSpawnDistance);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);
        }
    }
}