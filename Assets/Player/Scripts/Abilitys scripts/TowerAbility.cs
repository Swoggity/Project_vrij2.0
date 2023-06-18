using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAbility : MonoBehaviour
{
    public float fireRate = 0.2f; // Rate of fire in seconds
    public int damage = 2;
    public float speed = 10f;
    public Transform barrelExit;
    public GameObject hitMarker;
    public GameObject muzzleFlash;
    public GameObject projectilePrefab;
    public float endTime = 5;

    private float nextFireTime;
    private float startTime = 0;
    private float desiredDelay = 2.5f;
    private Animator mAnimator;
    private bool isFiring = false;
    private bool stopFiring = false;

    private void Start()
    {
        mAnimator = GetComponentInChildren<Animator>();
        startTime = Time.time;
        isFiring = false;
        mAnimator.SetBool("Exit", false);
    }

    private void Update()
    {
        if (!isFiring && Time.time > startTime + 2.5f)
        {
            isFiring = true;
        }

        if (isFiring == true && Time.time > nextFireTime)
        {
            Fire();
        }
        if (isFiring && Time.time > startTime + endTime)
        {
            stopFiring = true;
            mAnimator.SetBool("Exit", true);
            if (Time.time > startTime + endTime + 5)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Fire()
    {
        if (!stopFiring)
        {
            Debug.Log("Im firing while isfiring is " + isFiring);
            Instantiate(muzzleFlash, barrelExit.position, Quaternion.identity);

            GameObject projectile = Instantiate(projectilePrefab, barrelExit.position, barrelExit.rotation);
            PlayerBullet projectileComponent = projectile.GetComponent<PlayerBullet>();
            if (projectileComponent != null)
            {
                projectileComponent.SetDamage(damage, speed, hitMarker);
            }

            nextFireTime = Time.time + fireRate;
        }
    }
}
