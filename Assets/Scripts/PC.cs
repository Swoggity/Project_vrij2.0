using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f; //Target movement speed
    [SerializeField] private float moveAccel = 0.4f; //Acceleration to full speed in seconds
    [SerializeField] private float maxAttackingMoveSpeed, attackCooldown, attackDamage, attackKnockback, bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D body;
    private float attackTimer = 0f;
    public float moveMod = 1.0f; //Used for debuffs/buffs
    public float moveDir = 0.0f;

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        moveDir = Input.GetAxis("Horizontal");
        float targetmove = moveSpeed * moveDir * moveMod;
        float accel = (moveSpeed * Time.deltaTime) / moveAccel;
        if (body.velocity.x != targetmove)
        {
            if (Mathf.Abs(body.velocity.x - targetmove) < accel) { body.velocity = new Vector2(targetmove, 0); }
            else if (body.velocity.x < targetmove) { body.velocity += new Vector2(accel, 0); }
            else { body.velocity -= new Vector2(accel, 0); }
        }
    }

    private void Attack()
    {
        attackTimer = Mathf.Max(attackTimer - Time.deltaTime, 0);

        if(attackTimer <= 0 && Mathf.Abs(body.velocity.x) <= maxAttackingMoveSpeed)
        {
            attackTimer = attackCooldown;

            RangedAttack bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<RangedAttack>();

            bullet.targetLayer = LayerMask.GetMask("Enemies");
            bullet.damage = attackDamage;
            bullet.knockback = attackKnockback;
            bullet.speed = bulletSpeed;
            bullet.direction = Vector2.right;
        }
    }
}
