using System.Collections.Generic;
using UnityEngine;

public class EnemyFatCat : Enemy
{
    private float playerPosition;

    public override void Die()
    {
        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
        Instantiate(DeathEffect, transform.position, rotation);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (co.isGamePaused()) return;
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        DetectObstacle();
        playerPosition = playerObject.transform.position.x;
    }

    private void DetectObstacle()
    {
        if (co.becomeAlly)
        {
            isObstacleDetected = false;
            return;
        }
        if (transform.position.x <= playerPosition + adjustedDetectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (transform.position.x >= playerPosition + adjustedDetectionDistance + 1)
        {
            isObstacleDetected = false;
        }
    }

}