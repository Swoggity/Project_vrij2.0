using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRebel : Enemy
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
            if (!canAttack)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackRate)
                {
                    canAttack = true;
                }
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
        if (transform.position.x <= playerPosition.x + adjustedDetectionDistance + 0.2f)
        {
            isObstacleDetected = true;
        }
        else if (transform.position.x >= playerPosition.x + adjustedDetectionDistance + 2f)
        {
            isObstacleDetected = false;
        }
    }
    private void AttackPlayer()
    {
        co.loseScore(20, co.player.transform.position + new Vector3(0, 4, 0));
        /*Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(new Vector3(playerPosition.x + XOffset, playerPosition.y, playerPosition.z), attackRange, playerLayerMask);
        foreach (Collider2D playerCollider in hitPlayer)
        {
            if (playerCollider.gameObject.GetComponent<PlayerShoot>() != null)
            {
                //Code to lose points here
                co.loseScore(10, playerPosition);
                Debug.Log(this.name + " Is attacking");
            }
        }*/
    }
}