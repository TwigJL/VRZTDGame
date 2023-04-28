using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public Transform firePoint;
    public bool autoFire = true;

    public TowerBehavior towerBehavior; // Changed to public
    private float fireTimer;
    public float projectileSpeed;

    void Start()
    {
        // Removed the GetComponent<TowerBehavior>() line
        fireTimer = 0f;
    }

    void Update()
    {
        if (towerBehavior != null && towerBehavior.target != null && autoFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= 1f / fireRate)
            {
                FireProjectile(towerBehavior.target);
                fireTimer = 0f;
            }
        }
    }

    public void FireProjectile(Transform target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.SetTarget(target);
                projectileScript.SetSpeed(projectileSpeed); // Set the projectile speed
            }
        }
    }

}
