using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] float gridWidth;
    [SerializeField] GameObject basicPrefab;

    private GameObject[] towers = new GameObject[10];

    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            BuildTower();
        }
    }

    private void BuildTower()
    {
        int targetPosition = (int) Mathf.Round(transform.position.x / gridWidth);

        bool positionOnGrid = targetPosition >= 0 && targetPosition < towers.Length;

        if (positionOnGrid && !towers[targetPosition])
        {
            towers[targetPosition] = Instantiate(basicPrefab, new Vector2(targetPosition * gridWidth, 0), Quaternion.identity);
        }
    }
}
