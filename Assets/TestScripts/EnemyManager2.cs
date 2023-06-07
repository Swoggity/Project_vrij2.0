using UnityEngine;

public class EnemyManager2 : MonoBehaviour
{
    public GameObject fastPrefab;
    public GameObject strongPrefab;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        Instantiate(fastPrefab, new Vector3(10, -4, 0), Quaternion.identity);
        Instantiate(strongPrefab, new Vector3(7, -4, 0), Quaternion.identity);
    }
}
