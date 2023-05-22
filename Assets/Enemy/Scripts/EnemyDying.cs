using UnityEngine;

public class EnemyDying : BaseEnemy
{
    // The speed at which the enemy should spin
    public float spinSpeed = 50f;

    // The duration for which the enemy should spin before being destroyed
    public float spinDuration = 2f;

    // The time at which the enemy started spinning
    private float startTime;

    // The healthkit game object
    public GameObject healthKit;

    // Random number int
    private int randomNumber;

    public override void EnterState(EnemyManager Enemy)
    {
        // Record the start time
        startTime = Time.time;

        // Get the health kit object
        healthKit = GameObject.FindWithTag("healthKit");
        
        // Get random number
        randomNumber = Random.Range(1, 4);

    }

    public override void UpdateState(EnemyManager Enemy, int health)
    {
        // Calculate the elapsed time since the start of the spinning
        float elapsedTime = Time.time - startTime;

        // If the elapsed time is greater than the spin duration, destroy the enemy
        if (elapsedTime > spinDuration)
        {
            // 1/3 chance of spawning a health kit
            if (randomNumber == 3)
            {
                // Instantiate a health kit
                GameObject Healthkit = Object.Instantiate(healthKit, Enemy.transform.position, Quaternion.identity) as GameObject;
            }
            Object.Destroy(Enemy.gameObject);
            return;
        }

        // Spin the enemy around its y-axis
        Enemy.transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    public override void OnCollisionEnter(EnemyManager Enemy)
    {

    }

}
