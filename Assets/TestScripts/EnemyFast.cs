using UnityEngine;

public class EnemyFast : Enemy
{

    private float playerPosition;

    public override void Die()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        DetectObstacle();
        playerPosition = playerObject.transform.position.x;
    }

    private void DetectObstacle()
    {
        if (this.transform.position.x <= playerPosition + detectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (this.transform.position.x >= playerPosition + detectionDistance + 2)
        {
            isObstacleDetected = false;
        }

    }
}