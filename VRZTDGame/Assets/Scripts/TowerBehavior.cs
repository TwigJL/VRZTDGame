using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public string targetTag = "Zombie";

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
        if (target == null)
        {
            FindNewTarget();
            if (targetsInCollider.Count == 0)
            {
                animator.SetTrigger("Stop");
            }
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            animator.SetTrigger("Shoot");
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
