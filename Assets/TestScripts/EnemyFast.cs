using UnityEngine;

public class EnemyFast : Enemy
{



    public override void Die()
    {
        Debug.Log("Fast defeated!");
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            DetectObstacle();
        }
    }

    private void DetectObstacle()
    {
        if (this.transform.position.x <= playerObject.transform.position.x + detectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (this.transform.position.x >= playerObject.transform.position.x + detectionDistance + 2)
        {
            isObstacleDetected = false;
        }

    }
}