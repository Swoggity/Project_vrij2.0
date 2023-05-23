using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float projectileSpeed;

    private float attackDirectionVariance = 1f;

    override protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDuration);

        LobbedAttack attack = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<LobbedAttack>();

        attack.targetLayer = LayerMask.GetMask("Allies");
        attack.damage = attackDamage;
        attack.knockback = attackKnockback;

        float attackY = 1f + Random.Range(1f, 1f + attackDirectionVariance);
        attack.GetComponent<Rigidbody2D>().velocity = (new Vector2(-1f, attackY)).normalized * projectileSpeed;
    }
}
