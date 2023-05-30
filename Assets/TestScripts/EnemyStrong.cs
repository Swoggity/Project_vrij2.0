using UnityEngine;

public class EnemyStrong : Enemy
{
    public override void Die()
    {
        Debug.Log("Strong defeated!");
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            DetectObstacle();
        }
    }

    private void DetectObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionDistance);
        if (hit.collider != null)
        {
            isObstacleDetected = true;
            Debug.Log("Obstacle detected: " + hit.collider.gameObject.name);
        }
    }
}