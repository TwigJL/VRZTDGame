using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // Destroy the projectile or do other logic when the target is null
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }
}
