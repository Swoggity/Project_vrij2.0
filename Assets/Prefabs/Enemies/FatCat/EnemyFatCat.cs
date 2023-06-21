using System.Collections.Generic;
using UnityEngine;

public class EnemyFatCat : Enemy
{
    private Vector3 playerPosition;

    public override void Die()
    {
        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
        Instantiate(DeathEffect, transform.position, rotation);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (co.isGamePaused()) return;
        if (!isObstacleDetected)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (isObstacleDetected)
        {
            if (canAttack)
            {
                AttackPlayer();
                canAttack = false;
                attackTimer = 0f;
            }
        }
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                canAttack = true;
            }
        }
        DetectObstacle();
        playerPosition = playerObject.transform.position;
    }

    private void DetectObstacle()
    {
        if (co.becomeAlly)
        {
            isObstacleDetected = false;
            return;
        }
        if (transform.position.x <= playerPosition.x + adjustedDetectionDistance)
        {
            isObstacleDetected = true;
        }
        else if (transform.position.x >= playerPosition.x + adjustedDetectionDistance + 1)
        {
            isObstacleDetected = false;
        }
    }
    private void AttackPlayer()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(new Vector3(playerPosition.x + XOffset, playerPosition.y, playerPosition.z), attackRange, playerLayerMask);
        foreach (Collider2D playerCollider in hitPlayer)
        {
            IDamageable player = playerCollider.GetComponent<IDamageable>();
            if (player != null)
            {
                //Code to lose points here
                co.loseScore(100, playerPosition);
                Debug.Log(this.name + " Is attacking");
            }
        }
    }

}