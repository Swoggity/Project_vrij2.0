using UnityEngine;

public class EnemyFast : Enemy
{
    private float playerPosition;

    public void SetPlacementNumber(int placementNumber)
    {
        this.placementNumber = placementNumber;
    }

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
        Debug.Log(placementNumber);
    }

    private void DetectObstacle()
    {
        if (transform.position.x <= playerPosition + adjustedDetectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (transform.position.x >= playerPosition + adjustedDetectionDistance + 2)
        {
            isObstacleDetected = false;
        }
    }
}
