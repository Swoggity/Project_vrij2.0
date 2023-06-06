using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    // The amount of health that the object has
    public int health = 100;
    public Slider slider;

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;
    }
}
