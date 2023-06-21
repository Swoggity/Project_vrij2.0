using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSpeedBackMod = 0.4f; //Move backwards more slowly
    public float leftLimit = -9f;
    public float rightLimit = 9f;
    public bool isMainPlayer = true;
    CO co;
    private void Start()
    {
        co = FindObjectOfType<CO>();
    }
    private void Update()
    {
        float moveDirection = 0f;
        if (!co.isGamePaused() && isMainPlayer) //If game is not paused, move by player input
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                moveDirection = -moveSpeedBackMod; // Move left (more slowly)
            if (Input.GetKey(KeyCode.RightArrow))
                moveDirection = 1f; // Move right
        } else
        {
            moveDirection = co.MoveOverride(); //When the game is paused, movement direction is controlled by CO for use in cinematics
        }
        if (!co.becomeAlly) {
            Collider2D hitCols1 = Physics2D.OverlapBox(new Vector2(transform.position.x + 1.7f, transform.position.y), new Vector2(0.5f, 0.5f), 0f);
            if (hitCols1 != null) if (hitCols1.GetComponent<Enemy>()) moveDirection = Mathf.Clamp(moveDirection, -1, 0); //Check for collision in front of character
        }
        Vector3 currentPosition = transform.position;
        float targetPositionX = currentPosition.x + (moveSpeed * moveDirection * Time.deltaTime);
        targetPositionX = Mathf.Clamp(targetPositionX, leftLimit, rightLimit);
        transform.position = new Vector3(targetPositionX, currentPosition.y, currentPosition.z);
    }
}

