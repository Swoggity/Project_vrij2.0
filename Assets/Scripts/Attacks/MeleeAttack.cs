using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public LayerMask targetLayer;
    public float damage, knockback;
    public Vector3 direction;

    private float duration = 0.2f;

    private void Start()
    {
        Collider2D[] targetsHit = Physics2D.OverlapBoxAll(transform.position,transform.localScale, 0f, targetLayer);

        foreach(Collider2D target in targetsHit)
        {
            target.GetComponent<HealthManager>().GetHit(damage, knockback, direction);
        }
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f) Destroy(gameObject);
    }
}
