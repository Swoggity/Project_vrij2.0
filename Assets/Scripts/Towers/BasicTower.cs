using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [SerializeField] private float projectileSpeed;

    override protected IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDuration);

        RangedAttack attack = Instantiate(attackPrefab, transform.position + attackOriginOffset, Quaternion.Euler(targetDirection)).GetComponent<RangedAttack>();

        attack.targetLayer = LayerMask.GetMask("Enemies");
        attack.damage = attackDamage;
        attack.knockback = attackKnockback;
        attack.speed = projectileSpeed;
        attack.direction = targetDirection;
        attack.transform.rotation = Quaternion.Euler(targetDirection);
    }
}
