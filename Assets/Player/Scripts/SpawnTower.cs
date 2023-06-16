using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public GameObject towerPrefab; // Reference to the tower prefab
    public float TowerXOffset;

    public void Activate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //tower pivit offset x-14.24 y-0.22 z1.64
            Instantiate(towerPrefab, new Vector3 (player.transform.position.x + 14.24f + TowerXOffset, player.transform.position.y+ 0.22f + 1.55f, player.transform.position.z + 2), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player object not found");
        }
    }
}
