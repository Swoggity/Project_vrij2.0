using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab for the enemy GameObjects
    public Transform playerTransform; // The Transform component of the player
    public float spawnDistance = 10f; // The distance to spawn the enemies from the player
    public int waveSize = 5; // The number of enemies to spawn in each wave

    // Keep track of the number of enemies that have been spawned and destroyed
    public int enemiesSpawned = 0;
    public int enemiesDestroyed = 0;

    void Update()
    {
        // If all of the enemies in the current wave have been destroyed, spawn a new wave
        if (enemiesSpawned >= 0 && enemiesSpawned == enemiesDestroyed)
        {
            enemiesSpawned = 0;
            enemiesDestroyed = 0;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        // Increase the wave size by 1
        waveSize++;

        // Spawn the enemies in a circle around the player
        for (int i = 0; i < waveSize; i++)
        {
            float angle = i * Mathf.PI * 2 / waveSize;
            Vector3 spawnPosition = playerTransform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnDistance;

            // Instantiate the enemy prefab and store a reference to it
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Increment the enemiesSpawned counter
            enemiesSpawned++;

            // Wait for a short time before spawning the next enemy
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void EnemyDestroyed()
    {
        // Increment the enemiesDestroyed counter
        enemiesDestroyed++;
    }
}
