using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    override protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDuration);

        MeleeAttack attack = Instantiate(attackPrefab, transform.position - new Vector3(attackRange, 0), Quaternion.identity).GetComponent<MeleeAttack>();

        attack.targetLayer = LayerMask.GetMask("Allies");
        attack.damage = attackDamage;
        attack.knockback = attackKnockback;
        attack.direction = Vector2.left;
    }
}
