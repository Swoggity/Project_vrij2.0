using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    private void Start()
    {
        // Invoke the DestroySelf method after one second
        Invoke("DestroySelf", 1f);
    }

    private void DestroySelf()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}
