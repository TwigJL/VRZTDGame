using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 25;
    public LayerMask zombieLayer;
    private Transform target;
    public LayerMask projectileLayer;
    public float chainRadius = 5f;
    public float chainDelay = 0.5f;


    public enum ProjectileEffect
    {
        None,
        Slow,
        Freeze,
        Burn,
        AOE,
        Chain
    }
    public ProjectileEffect effect = ProjectileEffect.None;
 
    public float aoeRadius = 0f;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Collider[] zombies;
        // Check for AOE effect and increase the radius of the OverlapSphere accordingly
        if (effect == ProjectileEffect.AOE || effect == ProjectileEffect.Burn)
        {
            zombies = Physics.OverlapSphere(transform.position, aoeRadius, zombieLayer);
        }
        else
        {
            zombies = Physics.OverlapSphere(transform.position, 0.5f, zombieLayer);
        }
        foreach (Collider zombie in zombies)
        {
            ZombieBehavior zombieBehavior = zombie.GetComponent<ZombieBehavior>();
            if (zombieBehavior != null)
            {
                zombieBehavior.ApplyDamage(damage);
                // Apply the effect
                switch (effect)
                {
                    case ProjectileEffect.Slow:
                        zombieBehavior.ApplySlow();
                        break;
                    case ProjectileEffect.Freeze:
                        zombieBehavior.ApplyFreeze();
                        break;
                    case ProjectileEffect.Burn:
                        zombieBehavior.ApplyBurn();
                        break;
                     case ProjectileEffect.Chain:
                    zombieBehavior.ApplyChainEffect(damage, chainRadius, chainDelay);
                    break;
                }
                zombieBehavior.PlayEffectParticles(effect);
            }
        }

        Destroy(gameObject);
    }
    


}
