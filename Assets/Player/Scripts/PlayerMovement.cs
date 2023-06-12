using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float leftLimit = -9f;
    public float rightLimit = 9f;

    private void Update()
    {
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.Z))
            moveDirection = -1f; // Move left
        if (Input.GetKey(KeyCode.C))
            moveDirection = 1f; // Move right

        Vector3 currentPosition = transform.position;
        float targetPositionX = currentPosition.x + (moveSpeed * moveDirection * Time.deltaTime);
        targetPositionX = Mathf.Clamp(targetPositionX, leftLimit, rightLimit);
        transform.position = new Vector3(targetPositionX, currentPosition.y, currentPosition.z);
    }
}

