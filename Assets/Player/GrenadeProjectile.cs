using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    public int damage;
    public float radius;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach(Collider2D collider in targets)
            {
                IDamageable target = collider.GetComponent<IDamageable>();
                if (target != null) target.TakeDamage(damage);
            }

            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
