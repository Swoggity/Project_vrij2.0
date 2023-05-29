using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public int armor;
    public float speed;

    public void TakeDamage(int damage)
    {
        int damageTaken = damage - armor;
        if (damageTaken > 0)
        {
            health -= damageTaken;
            Debug.Log(gameObject.name + " takes " + damageTaken + " damage");
        }
        else
        {
            Debug.Log(gameObject.name + "Not strong enough");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
