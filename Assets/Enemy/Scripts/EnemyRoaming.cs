using UnityEngine;

public class EnemyRoaming : BaseEnemy
{

    // Speed of the object
    public float speed = 2.0f;

    // Minimum and maximum values for x and y axis
    public float xMin = -15.0f;
    public float xMax = 15.0f;
    public float yMin = -15.0f;
    public float yMax = 15.0f;

    // Target position
    private Vector3 targetPosition;

    // Players location
    public Transform player;
    public override void EnterState(EnemyManager Enemy)
    {
        // Get players location
        player = GameObject.FindWithTag("Player").transform;

        // Set the initial target position to the current position
        targetPosition = Enemy.transform.position;

        // Choose a new target position
        ChooseNewTargetPosition();

    }

    public override void UpdateState(EnemyManager Enemy, int health)
    {
        if (Vector3.Distance(Enemy.transform.position, player.position) <= 10)
        {
            Enemy.SwitchState(Enemy.ChasingState);
        }
        else
        {
            // Move towards the target position
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, targetPosition, speed * Time.deltaTime);

            // If the object has reached the target position, choose a new one
            if (Enemy.transform.position == targetPosition)
            {
                ChooseNewTargetPosition();
            }
        }
        if (health <= 30)
        {
            Enemy.SwitchState(Enemy.RetreatingState);
        }

    }

    public override void OnCollisionEnter(EnemyManager Enemy)
    {

    }
    void ChooseNewTargetPosition()
    {
        // Choose a random position within the specified range
        targetPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0);
    }

}
