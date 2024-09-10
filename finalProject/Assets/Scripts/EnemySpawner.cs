using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;               // Enemy prefab to spawn
    public Transform[] spawnPoints;              // Array of spawn points
    public int initialSpawnCount = 4;            // Initial number of enemies to spawn

    private List<GameObject> activeEnemies = new List<GameObject>();  // List to track active enemies

    void Start()
    {
        SpawnEnemies(initialSpawnCount);
    }

    void Update()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
                OnEnemyDestroyed();
            }
        }
    }

    void SpawnEnemies(int count)
    {
        System.Random rng = new System.Random();
        int n = spawnPoints.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Transform temp = spawnPoints[k];
            spawnPoints[k] = spawnPoints[n];
            spawnPoints[n] = temp;
        }

        for (int i = 0; i < count && i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
            activeEnemies.Add(enemy);
        }
    }

    void OnEnemyDestroyed()
    {
        if (activeEnemies.Count == 0) // Check if all enemies are destroyed
        {
            SpawnEnemies(initialSpawnCount);  // Respawn initial count of enemies
        }
    }
}
