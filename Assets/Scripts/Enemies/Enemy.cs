using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float movementSpeed, movementAcceleration, attackRange, attackCooldown, attackDuration, attackDamage, attackKnockback;
    [SerializeField] protected GameObject attackPrefab;
    protected abstract IEnumerator Attack();



    private const float verticalRange = 5f;
    private float currentAttackCooldown, currentAttackDuration = 0f;

    private void Update()
    {
        TryAttack();
        TryMove();
    }

    private void TryAttack()
    {
        if (currentAttackDuration > 0f) // Wait until current attack is finished
        {
            currentAttackDuration = Mathf.Max(currentAttackDuration - Time.deltaTime, 0f);
        } else if (currentAttackCooldown > 0f) // Wait until attack is off cooldown
        {
            currentAttackCooldown = Mathf.Max(currentAttackCooldown - Time.deltaTime, 0f);
        } else if(EnemyInRange())
        {
            currentAttackDuration = attackDuration;
            currentAttackCooldown = attackCooldown;

            StartCoroutine(Attack());
        }
    }
    private void TryMove()
    {
        float targetSpeed = (currentAttackDuration > 0f || EnemyInRange()) ? 0f : -1 * movementSpeed; // stand still if attacking or enemy is close

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        float trueAcceleration = movementAcceleration * Time.deltaTime;
        float speedDifference = body.velocity.x - targetSpeed;

        if (Mathf.Abs(speedDifference) <= trueAcceleration)
        {
            body.velocity = new Vector2(targetSpeed, 0);
        } else
        {
            body.velocity -= new Vector2(trueAcceleration * Mathf.Sign(speedDifference), 0);
        }
    }

    private bool EnemyInRange()
    {
        Vector2 topLeftOfRange = new Vector2(transform.position.x - attackRange, transform.position.y + verticalRange);
        Vector2 bottomRightOfRange = new Vector2(transform.position.x, transform.position.y - verticalRange);

        return Physics2D.OverlapArea(topLeftOfRange, bottomRightOfRange, LayerMask.GetMask("Allies"));
    }

}
