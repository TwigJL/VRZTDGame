using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 1f;
    public Transform firePoint;
    public bool autoFire = true;

    private TowerBehavior towerBehavior;
    private float fireTimer;

    void Start()
    {
        towerBehavior = GetComponent<TowerBehavior>();
        fireTimer = 0f;
    }

    void Update()
    {
        if (towerBehavior.target != null && autoFire)
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
            }
        }
    }
}
