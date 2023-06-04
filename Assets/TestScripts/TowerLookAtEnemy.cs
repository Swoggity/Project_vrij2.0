using System.Collections.Generic;
using UnityEngine;

public class TowerLookAtEnemy : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public Transform lookAtTarget;
    public List<GameObject> enemiesList = new List<GameObject>();
    public float rotationSpeed = 100f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            enemiesList.Add(other.gameObject);
            UpdateLookAtTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            enemiesList.Remove(other.gameObject);
            UpdateLookAtTarget();
        }
    }

    private void OnDestroy()
    {
        // Remove destroyed enemies from the list
        enemiesList.RemoveAll(TheEnemy => TheEnemy == null);
        UpdateLookAtTarget();
    }

    private void UpdateLookAtTarget()
    {
        if (enemiesList.Count > 0)
        {
            lookAtTarget = enemiesList[0].transform;
        }
        else
        {
            lookAtTarget = null;
        }
    }

    private void Update()
    {
        if (lookAtTarget != null)
        {
            // Get the direction to the target
            Vector3 direction = lookAtTarget.position - transform.position;

            // Project the direction onto the XZ plane
            Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z).normalized;

            // Calculate the target rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionXZ);

            // Smoothly rotate towards the target rotation
            transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Debug.Log(targetRotation);
        }
        else
        {
            transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, Quaternion.Euler(0f, 120f, 0f), rotationSpeed * Time.deltaTime);
        }
    }
}
