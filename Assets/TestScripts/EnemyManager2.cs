using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
    public int difficultyLevel;
    public float timeBetweenWaves;
    public List<Wave> waves;

    private int currentWaveIndex;
    private bool spawningWave;
    private float waveTimer;

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

    private void Start()
    {
        StartWaveByDifficulty(difficultyLevel);
    }

    private void Update()
    {
        if (spawningWave)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                spawningWave = false;
                StartWaveByDifficulty(difficultyLevel);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            difficultyLevel++;
            if (difficultyLevel > 5)
            {
                difficultyLevel = 1;
            }
        }
    }

    private void StartWaveByDifficulty(int requiredDifficulty)
    {
        List<Wave> validWaves = GetValidWaves(requiredDifficulty);
        if (validWaves.Count > 0)
        {
            currentWaveIndex = Random.Range(0, validWaves.Count);
            StartWave(currentWaveIndex);
        }
    }

    private void StartWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        StartCoroutine(SpawnWave(wave));
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        spawningWave = true;
        waveTimer = timeBetweenWaves;

        foreach (EnemySpawnData spawnData in wave.enemySpawnData)
        {
            for (int i = 0; i < spawnData.enemyCount; i++)
            {
                Instantiate(spawnData.enemyPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f); // Adjust delay between enemy spawns
            }
        }
    }

    private List<Wave> GetValidWaves(int requiredDifficulty)
    {
        List<Wave> validWaves = new List<Wave>();
        foreach (Wave wave in waves)
        {
            if (wave.enemySpawnData.Count > 0)
            {
                validWaves.Add(wave);
            }
        }
        return validWaves;
    }
}
