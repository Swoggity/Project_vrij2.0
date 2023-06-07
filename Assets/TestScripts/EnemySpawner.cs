using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public int difficultyLevel;
    public float timeBetweenWaves;
    public List<Wave> waves;

    private int currentWaveIndex;
    private bool spawningWave;
    private float waveTimer;

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        // Check if the current wave has finished spawning
        if (spawningWave)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                spawningWave = false;
                StartNextWave();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            difficultyLevel++;
            if (difficultyLevel > 5)
            {
                difficultyLevel = 0;
            }
        }
    }

    void StartNextWave()
    {
        int waveIndex = difficultyLevel % waves.Count; // Calculate the wave index based on difficulty level

        Wave currentWave = waves[waveIndex];
        StartCoroutine(SpawnEnemies(currentWave));

        spawningWave = true;
        waveTimer = timeBetweenWaves;
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        foreach (EnemySpawnData spawnData in wave.enemySpawnData)
        {
            for (int i = 0; i < spawnData.enemyCount; i++)
            {
                Instantiate(spawnData.enemyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f); // Adjust the delay duration as desired
            }
        }
    }

    [System.Serializable]
    public class Wave
    {
        public List<EnemySpawnData> enemySpawnData;
    }

    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab;
        public int enemyCount;
    }
}
