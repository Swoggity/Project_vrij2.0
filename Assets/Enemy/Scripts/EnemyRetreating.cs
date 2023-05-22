using UnityEngine;

public class EnemyRetreating : BaseEnemy
{
    // The distance that the enemy should move away from the player
    public float moveAwayDistance = 10f;

    // The maximum distance that the enemy can move away from the player
    public float maxMoveAwayDistance = 20f;

    // The speed at which the enemy should move
    public float moveSpeed = 5f;

    // The player transform
    public Transform player;


    public override void EnterState(EnemyManager Enemy)
    {
        // Get a reference to the player's transform
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    public override void UpdateState(EnemyManager Enemy, int health)
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector2.Distance(Enemy.transform.position, player.position);

        // If the enemy is already further away from the player than the maximum distance, do not move
        if (distance >= maxMoveAwayDistance)
        {
            return;
        }

        // Calculate the direction that the enemy should move in
        Vector2 moveDirection = (Enemy.transform.position - player.position).normalized;

        // Calculate the target position that the enemy should move to
        Vector2 targetPosition = (Vector2)Enemy.transform.position + moveDirection * moveAwayDistance;

        // If the target position is further away from the player than the maximum distance, clamp it to the maximum distance
        if (Vector2.Distance(targetPosition, player.position) > maxMoveAwayDistance)
        {
            targetPosition = (Vector2)player.position + moveDirection * maxMoveAwayDistance;
        }

        // Move the enemy towards the target position
        Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (health <= 0)
        {
            Enemy.SwitchState(Enemy.DyingState);
        }

    
    }

    public override void OnCollisionEnter(EnemyManager Enemy)
    {

    }

}
