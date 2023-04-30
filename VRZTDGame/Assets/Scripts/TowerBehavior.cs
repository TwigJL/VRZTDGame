using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public string targetTag = "Zombie";
    public bool trackTarget = true;
    public Transform target;
    private SphereCollider towerRange;
    private List<Transform> targetsInCollider = new List<Transform>();
    private Animator animator;

    void Start()
    {
        towerRange = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Always try to find a new target
        if (target == null || target.GetComponent<ZombieBehavior>().isDead)
        {
            FindNewTarget();
        }

        // Rotate the tower based on the trackTarget value
        if (!trackTarget)
        {
            // Rotate around Y-axis at a fixed speed when not tracking a target
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
        else
        {
            if (target != null )
            {
                Vector3 direction = target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Vector3 rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                if (animator != null)
                {
                    animator.SetTrigger("Shoot");
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("Stop");
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            targetsInCollider.Add(other.transform);
            if (target == null)
            {
                target = other.transform;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            targetsInCollider.Remove(other.transform);
            if (target == other.transform)
            {
                target = null;
            }
        }
    }

    void FindNewTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Transform zombie in targetsInCollider)
        {
            if (zombie != null)
            {
                float distance = Vector3.Distance(transform.position, zombie.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = zombie;
                }
            } 
        }
        target = closestTarget;
    }
}
