using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int difficultyLevel;
    public float timeBetweenWaves;
    public int groupSize; // Maximum number of enemies in a group
    public List<Wave> waves;

    private int placementNumber = 0;
    private int currentWaveIndex;
    private bool spawningWave;
    public float waveTimer;

    public List<EnemyGroup> enemyGroups; // List to store enemy groups

    void Start()
    {
        enemyGroups = new List<EnemyGroup>();
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

        /*if (Input.GetKeyDown(KeyCode.X))
        {
            difficultyLevel++;
            if (difficultyLevel > 5)
            {
                difficultyLevel = 0;
            }
        }*/
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
                if (enemyGroups.Count == 0 || enemyGroups[enemyGroups.Count - 1].IsFull())
                {
                    // Create a new enemy group if the last group is full or there are no groups yet
                    EnemyGroup newGroup = new EnemyGroup(groupSize);
                    enemyGroups.Add(newGroup);
                    placementNumber = 0; // Reset the placement number when a new group is created
                }

                // Get the current group and add the enemy to it
                EnemyGroup currentGroup = enemyGroups[enemyGroups.Count - 1];
                GameObject enemyInstance = Instantiate(spawnData.enemyPrefab, transform.position, Quaternion.identity);
                currentGroup.AddEnemy(enemyInstance);

                // Set the placement number for the enemy
                Enemy enemy = enemyInstance.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.SetPlacementNumber(currentGroup.enemies.Count - 1); // Set the placement number to the index of the enemy in the group
                }

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

    // Class to represent an enemy group
    public class EnemyGroup
    {
        public List<GameObject> enemies; // Change access modifier to public
        private int maxSize;

        public EnemyGroup(int maxSize)
        {
            this.maxSize = maxSize;
            enemies = new List<GameObject>();
        }

        public bool IsFull()
        {
            return enemies.Count >= maxSize;
        }

        public void AddEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
            // Set the position or do any additional setup for the enemy within the group
        }
    }
}