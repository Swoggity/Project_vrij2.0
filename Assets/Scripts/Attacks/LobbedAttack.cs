using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbedAttack : MonoBehaviour
{
    public LayerMask targetLayer;
    public float damage, knockback, speed;
    public Vector3 direction;

    private float radius = 0.1f;
    private float duration = 5f;

    private void FixedUpdate()
    {
        CheckHit();
        UpdateDuration();
    }

    private void CheckHit()
    {
        Collider2D targetHit = Physics2D.OverlapCircle(transform.position, radius, targetLayer);

        if (targetHit)
        {
            targetHit.GetComponent<HealthManager>().GetHit(damage, knockback, direction);
            Destroy(gameObject);
        }
    }
    private void UpdateDuration()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f) Destroy(gameObject);
    }
}
