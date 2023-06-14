using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f; // Rate of fire in seconds
    public float cooldownTime = 0.5f; // Cooldown time in seconds
    public int damage = 2;
    public float speed = 10f;
    public Transform barrelExit;
    public GameObject hitMarker;
    public GameObject muzzleFlash;
    public GameObject projectilePrefab;
    CO co;

    private float nextFireTime;
    public bool isFiring = false;

    private void Start()
    {
        co = FindObjectOfType<CO>();
    }
    private void Update()
    {
        if (co.isGamePaused()) return;
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            isFiring = true;
            Fire();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFiring = false;
        }

        if (isFiring && Time.time > nextFireTime)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(muzzleFlash, barrelExit.position, Quaternion.identity);

        GameObject projectile = Instantiate(projectilePrefab, barrelExit.position, barrelExit.rotation);
        PlayerBullet projectileComponent = projectile.GetComponent<PlayerBullet>();
        if (projectileComponent != null)
        {
            projectileComponent.SetDamage(damage, speed, hitMarker);
        }

        // Apply bullet spread by modifying the rotation
        float spreadAngle = Random.Range(-10f, 10f);
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, barrelExit.eulerAngles.z + spreadAngle);
        projectile.transform.rotation = spreadRotation;

        nextFireTime = Time.time + fireRate;
        if (!isFiring)
        {
            nextFireTime += cooldownTime;
        }
    }
}
