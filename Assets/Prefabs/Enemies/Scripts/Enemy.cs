using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int armor;
    public float speed;
    public bool isObstacleDetected = false;
    public float detectionDistance;
    public float adjustedDetectionDistance;
    public int placementNumber; // Placement number within the group

    public GameObject playerObject;
    public GameObject DeathEffect;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        adjustedDetectionDistance = detectionDistance + ((placementNumber + Random.Range(0.5f, 1.5f)) * 1f); // Adjust detection distance based on placement number
    }

    public void SetPlacementNumber(int placementNumber)
    {
        this.placementNumber = placementNumber;
    }



    public void TakeDamage(int damage)
    {
        int damageTaken = damage - armor;
        if (damageTaken > 0)
        {
            health -= damageTaken;
            //Debug.Log(gameObject.name + " takes " + damageTaken + " damage");
        }
        else
        {
            //Debug.Log(gameObject.name + "Not strong enough");
        }

        if (health <= 0)
        {
            Die();

        }
    }

    public abstract void Die();
}
