using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f; // Rate of fire in seconds
    public float cooldownTime = 0.5f; // Cooldown time in seconds
    public int damage = 2;
    public Transform barrelExit;
    public GameObject hitMarker;
    public ParticleSystem particleSystem;

    public float nextFireTime; // Time of the next allowed fire
    public bool isFiring = false; // Flag indicating if player is firing

    private void Start()
    {
        particleSystem.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            isFiring = true;
            Fire();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFiring = false;
            particleSystem.Stop();
        }

        if (isFiring && Time.time > nextFireTime)
        {
            Fire();
        }
    }

    private void Fire()
    {
        particleSystem.Play(); // Play the particle system when the player is firing

        RaycastHit2D[] hits = Physics2D.RaycastAll(barrelExit.position, barrelExit.right);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                Instantiate(hitMarker, new Vector3(hit.collider.transform.position.x + 0.5f, barrelExit.position.y, 0), Quaternion.identity);
                damageableObject.TakeDamage(damage);
                break;
            }
        }
        Debug.DrawRay(barrelExit.position, barrelExit.right * hits[0].distance, Color.red, 0.2f);

        nextFireTime = Time.time + fireRate;
        if (!isFiring)
        {
            nextFireTime += cooldownTime;
        }
    }
}
