using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // The amount of health that the object has
    public int health = 100;
    public Slider slider;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "bullet" tag
        if (other.tag == "Bullet")
        {
            // Decrease the object's health by 10
            health -= 10;
            slider.value = health;
        }
    }
}
