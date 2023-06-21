using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryAbility : MonoBehaviour
{
    public float range = 2f; 
    public int damageAmount = 10;
    public float XOffset;
    public float YOffset;
    public Transform PlayerPosition;

    public LayerMask enemyLayerMask; // Layer mask for enemy objects

    public void PerformParryAttack()
    {
        // Cast a circle in front of the player to detect enemies within range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(new Vector3(PlayerPosition.position.x + XOffset, PlayerPosition.position.y+ YOffset, PlayerPosition.position.z), range, enemyLayerMask);
        Debug.Log(new Vector3(PlayerPosition.position.x + XOffset, PlayerPosition.position.y, PlayerPosition.position.z));

        // Damage all enemies with IDamageable component
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            IDamageable enemy = enemyCollider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }

        //make player invulnerable
    }

    // Draw the range of the parry attack in the scene view for debugging
    public void OnDrawGizmos()
    {
        if (PlayerPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(PlayerPosition.position.x + XOffset, PlayerPosition.position.y+ YOffset, PlayerPosition.position.z), range);
    }
}