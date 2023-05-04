using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public float[] fireRates; // Changed to an array of floats
    public Transform firePoint;
    public bool autoFire = true;

    public TowerBehavior towerBehavior;
    private float fireTimer;
    public float projectileSpeed;

    void Start()
    {
        fireTimer = 0f;
    }

    void Update()
    {
        if (towerBehavior != null && towerBehavior.target != null && autoFire)
        {
            int towerLevel = towerBehavior.towerLevel;

            if (towerLevel > 0 && towerLevel <= fireRates.Length)
            {
                float fireRate = fireRates[towerLevel - 1]; // Get the fire rate based on the tower level

                fireTimer += Time.deltaTime;
                if (fireTimer >= 1f / fireRate)
                {
                    FireProjectile(towerBehavior.target);
                    fireTimer = 0f;
                }
            }
        }
    }

    public void FireProjectile(Transform target)
    {
        int towerLevel = towerBehavior.towerLevel;

        if (towerLevel > 0 && towerLevel <= projectilePrefabs.Length)
        {
            GameObject projectilePrefab = projectilePrefabs[towerLevel - 1];

            if (projectilePrefab != null && firePoint != null)
            {
                GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetTarget(target);
                    projectileScript.SetSpeed(projectileSpeed);
                }
            }
        }
    }
}
