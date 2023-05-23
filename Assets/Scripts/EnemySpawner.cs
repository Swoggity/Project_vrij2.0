using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private string[] waves;

    [SerializeField] private float waveDuration, spawnPositionOffset, spawnDelay;
    [SerializeField] private Transform player, enemies;
    [SerializeField] private GameObject protesterPrefab, fatcatPrefab, molotovPrefab;

    private int currentWave, activeWaveCount = 0;
    private float waveTimer = 0f;

    private void Update()
    {
        UpdatePosition();
        UpdateWave();
    }

    private void UpdatePosition()
    {
        transform.position = new Vector3(Mathf.Max(player.position.x + spawnPositionOffset, transform.position.x), transform.position.y);
    }
    private void UpdateWave()
    {
        waveTimer -= Time.deltaTime;

        bool waveIsCleared = (activeWaveCount == 0 && enemies.childCount == 0);

        if (waveIsCleared || waveTimer <= 0)
        {
            StartCoroutine(SpawnNextWave());
            waveTimer = waveDuration;
        }
    }

    private IEnumerator SpawnNextWave()
    {
        if (currentWave >= waves.Length) yield break;

        activeWaveCount++;

        string wave = waves[currentWave];
        currentWave++;
        foreach(char c in wave)
        {
            SpawnEnemy(c);
            yield return new WaitForSeconds(spawnDelay);
        }

        activeWaveCount--;
    }

    private void SpawnEnemy(char c)
    {
        GameObject newEnemy = null;

        switch(c)
        {
            case 'P':
                newEnemy = Instantiate(protesterPrefab, transform.position, Quaternion.identity);
                break;
            case 'F':
                newEnemy = Instantiate(fatcatPrefab, transform.position, Quaternion.identity);
                break;
            case 'M':
                newEnemy = Instantiate(molotovPrefab, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }

        if (newEnemy) newEnemy.transform.parent = enemies;
    }
}
