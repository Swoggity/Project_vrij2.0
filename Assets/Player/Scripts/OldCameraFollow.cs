using UnityEngine;

public class OldCameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    private bool isFollowing;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (!isFollowing)
        {
            if (player.position.x >= 0 && player.position.x <= 10)
                isFollowing = true;
        }

        if (isFollowing)
        {
            Vector3 targetPosition = player.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, 0f, 10f);
            transform.position = targetPosition;
        }
    }
}
