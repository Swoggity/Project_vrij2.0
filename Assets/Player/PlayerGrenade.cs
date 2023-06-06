using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : PlayerAbility
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int damage;
    [SerializeField] private float radius;
    [SerializeField] private Vector2 velocity;
    protected override IEnumerator Activate()
    {
        GrenadeProjectile grenade = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<GrenadeProjectile>();

        grenade.damage = damage;
        grenade.radius = radius;
        grenade.GetComponent<Rigidbody2D>().velocity = velocity;

        yield return null;
    }
}
