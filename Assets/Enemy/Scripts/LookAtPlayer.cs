using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;// The players location
    public float range = 10.0f; // The maximum range at which the enemy will look towards the player
    public float rotationSpeed = 10.0f; // The speed at which the enemy will rotate to look at the player

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is within the specified range
        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            // Calculate the angle to look at the player
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate only on the z-axis to look at the player smoothly
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
