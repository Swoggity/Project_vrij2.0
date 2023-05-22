using System.Collections;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // Prefab of the bullet to be shot
    public GameObject bulletPrefab;

    // Minimum and maximum time interval between shots
    public float minTimeInterval = 1f;
    public float maxTimeInterval = 3f;

    // Time elapsed since the last shot
    private float timeSinceLastShot = 0f;

    // The Transform component of the object that will shoot the bullet
    private Transform shooterTransform;

    void Start()
    {
        // Get the Transform component of the object
        shooterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // Increment the time elapsed since the last shot
        timeSinceLastShot += Time.deltaTime;

        // If the time elapsed is greater than the time interval between shots, shoot a bullet
        if (timeSinceLastShot >= Random.Range(minTimeInterval, maxTimeInterval))
        {
            // Reset the time elapsed since the last shot
            timeSinceLastShot = 0f;

            // Instantiate a new bullet at the position and rotation of the shooter
            GameObject bullet = Instantiate(bulletPrefab, shooterTransform.position, shooterTransform.rotation);

            // Add force to the bullet in the forward direction of the shooter
            bullet.GetComponent<Rigidbody>().AddForce(shooterTransform.forward * 1000f);
        }
    }
}
