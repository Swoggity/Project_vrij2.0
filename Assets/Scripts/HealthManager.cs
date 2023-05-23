using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float health;

    private TextMesh healthDisplay;
    private Rigidbody2D body;

    private void Awake()
    {
        healthDisplay = new GameObject("Health Display").AddComponent<TextMesh>();
        healthDisplay.transform.parent = transform;
        healthDisplay.transform.localPosition = Vector3.zero;
        healthDisplay.offsetZ = -1;

        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health <= 0f)
        {
            Destroy(gameObject);
        }

        healthDisplay.text = health.ToString();
    }

    public void GetHit(float damage, float knockback, Vector2 direction)
    {
        health -= damage;

        body.velocity += direction.normalized * knockback;
    }
}
