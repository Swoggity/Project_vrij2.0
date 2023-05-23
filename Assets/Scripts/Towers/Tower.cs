using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float attackRange, attackCooldown, attackDuration, attackDamage, attackKnockback;
    [SerializeField] protected Vector3 attackOriginOffset;
    [SerializeField] protected GameObject attackPrefab;
    protected abstract IEnumerator Attack();



    private const float verticalRange = 5f;
    private float currentAttackCooldown, currentAttackDuration = 0f;

    protected Vector2 targetDirection = Vector2.right;

    private void Update()
    {
        TrackTarget();
        TryAttack();
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
    private void TrackTarget()
    {
        Vector2 topLeftOfRange = new Vector2(transform.position.x - attackRange, transform.position.y + verticalRange);
        Vector2 bottomRightOfRange = new Vector2(transform.position.x + attackRange, transform.position.y - verticalRange);

        Collider2D[] targets = Physics2D.OverlapAreaAll(topLeftOfRange, bottomRightOfRange, LayerMask.GetMask("Enemies"));

        float minDistance = float.MaxValue;
        
        foreach(Collider2D target in targets)
        {
            float distance = (target.transform.position - attackOriginOffset - transform.position).magnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                targetDirection = (target.transform.position - attackOriginOffset - transform.position).normalized;
            }
        }
    }

    private bool EnemyInRange()
    {
        Vector2 topLeftOfRange = new Vector2(transform.position.x - attackRange, transform.position.y + verticalRange);
        Vector2 bottomRightOfRange = new Vector2(transform.position.x + attackRange, transform.position.y - verticalRange);

        return Physics2D.OverlapArea(topLeftOfRange, bottomRightOfRange, LayerMask.GetMask("Enemies"));
    }

}
