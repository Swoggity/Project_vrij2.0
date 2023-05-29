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
        Instantiate(fastPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(strongPrefab, new Vector3(2, 0, 0), Quaternion.identity);
    }
}
