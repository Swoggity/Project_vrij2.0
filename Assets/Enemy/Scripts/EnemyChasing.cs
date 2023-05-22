using UnityEngine;

public class EnemyChasing : BaseEnemy
{
    private Transform player;// The players location
    private float rotationSpeed = 100.0f; // The speed at which the enemy will rotate to look at the player

    public float moveSpeed = 5f; // The speed at which the enemy moves
    public float minRadius = 3f; // The minimum radius around the player that the enemy should stay within
    public float maxRadius = 10f; // The maximum radius around the player that the enemy should stay within

    private Vector3 randomDirection;// A vector representing the enemy's current random movement direction
    private float randomRadius; // A random radius within the min and max radius range

    //Prefab of the bullet to be shot
    public GameObject bulletPrefab;

    //Minimum and maximum time interval between shots
    public float minTimeInterval = 1f;
    public float maxTimeInterval = 3f;

    //Time elapsed since the last shot
    private float timeSinceLastShot = 0f;

    private bool inRange = false;

    public override void EnterState(EnemyManager Enemy)
    {
        //Find the player game object and store its transform component
        player = GameObject.FindWithTag("Player").transform;

        bulletPrefab = GameObject.FindWithTag("Bullet");

        //Choose a random radius within the min and max radius range
        randomRadius = Random.Range(minRadius, maxRadius);

        //Choose a random movement direction
        randomDirection = Random.insideUnitCircle;
    }

    public override void UpdateState(EnemyManager Enemy, int health)
    {
        //Calculate the angle to look at the player
        Vector3 direction = player.position - Enemy.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Rotate only on the z-axis to look at the player smoothly
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        Enemy.transform.rotation = Quaternion.RotateTowards(Enemy.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(Enemy.transform.position, player.position);

        //If the distance is greater than the random radius, move towards the player
        if (distance > randomRadius)
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, player.position, moveSpeed * Time.deltaTime);
            inRange = true;
        }
        //If the distance is within the random radius, move randomly within the radius
        else
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Enemy.transform.position + randomDirection, moveSpeed * Time.deltaTime);

            //If the enemy reaches the edge of the radius, choose a new random movement direction
            if (distance <= minRadius)
            {
                randomDirection = Random.insideUnitCircle;
            }
        }
        if (inRange == true)
        {
            //Increment the time elapsed since the last shot
            timeSinceLastShot += Time.deltaTime;

            //If the time elapsed is greater than the time interval between shots, shoot a bullet
            if (timeSinceLastShot >= Random.Range(minTimeInterval, maxTimeInterval))
            {
                // Reset the time elapsed since the last shot
                timeSinceLastShot = 0f;

                // Instantiate a new bullet at the position and rotation of the shooter
                GameObject bullet = Object.Instantiate(bulletPrefab, Enemy.transform.position, Enemy.transform.rotation) as GameObject;

                //Debug.Log(Enemy.transform.rotation);

                // Add force to the bullet in the forward direction of the shooter
                //bullet.AddComponent<SelfDestruct>();
                //bullet.GetComponent<Rigidbody>().AddForce(Enemy.transform.forward * 1000f);
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

}
