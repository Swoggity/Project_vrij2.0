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
        Debug.Log("shooting ray");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, detectionDistance);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            isObstacleDetected = true;
            Debug.Log("Obstacle detected: " + hit.collider.gameObject.name);
        }
    }
}