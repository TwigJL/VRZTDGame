using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public Waypoint[] waypoints;
    private Animator animator;
    private int currentWaypoint = 0;
    public float slowDuration = 2f;
    public float freezeDuration = 3f;
    public float burnDuration = 5f;
    public int burnDamagePerSecond = 10;
    public int maxHealth = 100;
    public int health;
    public int zombieValue = 100;
    public GameManager gameManager;
    public int damage = 5;
    private bool isSlowed = false;
    private float normalSpeed;
    private Coroutine slowCoroutine;
    private Coroutine freezeCoroutine;
    private Coroutine burnCoroutine;
    public LayerMask zombieLayer;
    public ParticleSystem slowEffectParticles;
    public ParticleSystem freezeEffectParticles;
    public ParticleSystem burnEffectParticles;
    public ParticleSystem chainEffectParticles;
    public ParticleSystem aoeEffectParticles;
    public ParticleSystem bleedParticles;
    private Vector3 nextTargetPosition;
    public bool isDead = false;
   public float immuneStateDuration = 3.0f;
   private float immuneStateEndTime;
   private bool isFrozen = false;

   private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        normalSpeed = speed;
        health = (int)(maxHealth * Mathf.Log(gameManager.waveCT + 1, 2));
        animator.SetFloat("Speed", speed);
        zombieLayer = LayerMask.GetMask("Zombie");
        DisableAllEffectParticles();
        
    }
    private void DisableAllEffectParticles()
    {
        
        slowEffectParticles.Stop();
        freezeEffectParticles.Stop();
        burnEffectParticles.Stop();
        aoeEffectParticles.Stop();
        chainEffectParticles.Stop();
        bleedParticles.Stop();

        slowEffectParticles.gameObject.SetActive(false);
        freezeEffectParticles.gameObject.SetActive(false);
        burnEffectParticles.gameObject.SetActive(false);
        aoeEffectParticles.gameObject.SetActive(false);
        chainEffectParticles.gameObject.SetActive(false);
        bleedParticles.gameObject.SetActive(false);
    }
    void Update()
    {
        if (waypoints == null || currentWaypoint >= waypoints.Length || waypoints[currentWaypoint] == null)
        {
            return;
        }

        if (currentWaypoint < waypoints.Length)
        {
            Vector3 direction = new Vector3(waypoints[currentWaypoint].transform.position.x - transform.position.x, 0, waypoints[currentWaypoint].transform.position.z - transform.position.z);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        
    }



    void OnTriggerEnter(Collider other)
    {
        if (currentWaypoint < waypoints.Length && waypoints[currentWaypoint] != null &&
        other.gameObject == waypoints[currentWaypoint].gameObject)
        {
            ChooseNextWaypoint();
            
        }
      if (other.gameObject.CompareTag("LastWaypoint"))
      {
         gameManager.health -= damage;
      }
    }

    void ChooseNextWaypoint()
    {
        Waypoint current = waypoints[currentWaypoint];
        if (current.connectedWaypoints.Length > 0)
        {
            int nextIndex = Random.Range(0, current.connectedWaypoints.Length);
            currentWaypoint = System.Array.IndexOf(waypoints, current.connectedWaypoints[nextIndex]);
        }
        else
        {
            currentWaypoint++;
        }
        if (currentWaypoint >= waypoints.Length)
        {
            DestroyGameObject(); // destroy the game object if the last waypoint is reached
            return;
        }

        if (currentWaypoint < waypoints.Length && waypoints[currentWaypoint] != null)
        {
            nextTargetPosition = waypoints[currentWaypoint].transform.position;
        }
    }

   public void ApplySlow()
   {
      if (!isSlowed && Time.time > immuneStateEndTime)
      {
         if (slowCoroutine != null) StopCoroutine(slowCoroutine);
         if (slowCoroutine == null)
         {
            slowEffectParticles.gameObject.SetActive(true);
            slowEffectParticles.Play();
            slowCoroutine = StartCoroutine(SlowEffect());
         }
         isSlowed = true;
      }
   }

   private IEnumerator SlowEffect()
   {
      animator.SetFloat("Speed", normalSpeed / 2f);
      yield return new WaitForSeconds(slowDuration);
      slowEffectParticles.Stop();
      slowEffectParticles.gameObject.SetActive(false);
      animator.SetFloat("Speed", normalSpeed);
      immuneStateEndTime = Time.time + immuneStateDuration;
      isSlowed = false;
   }


   public void ApplyFreeze()
   {
      if (!isFrozen && Time.time > immuneStateEndTime)
      {
         if (freezeCoroutine != null) StopCoroutine(freezeCoroutine);
         freezeCoroutine = StartCoroutine(FreezeEffect());
      }
   }

   private IEnumerator FreezeEffect()
   {
      isFrozen = true;
      animator.SetFloat("Speed", 0f);
      freezeEffectParticles.gameObject.SetActive(true);
      freezeEffectParticles.Play();
      yield return new WaitForSeconds(freezeDuration);
      freezeEffectParticles.Stop();
      freezeEffectParticles.gameObject.SetActive(false);
      animator.SetFloat("Speed", normalSpeed);
      immuneStateEndTime = Time.time + immuneStateDuration;
      isFrozen = false;
      freezeCoroutine = null; // reset freezeCoroutine to null
   }

   public void ApplyBurn()
   {
      if (Time.time > immuneStateEndTime)
      {
         // If burnCoroutine is not null, stop the running coroutine
         if (burnCoroutine != null)
         {
            StopCoroutine(burnCoroutine);
         }

         // Activate and play the burn effect particle system
         burnEffectParticles.gameObject.SetActive(true);
         burnEffectParticles.Play();

         // Start the BurnEffect coroutine
         burnCoroutine = StartCoroutine(BurnEffect());
      }
   }

   private IEnumerator BurnEffect()
   {
      float burnTime = 0f;

      while (burnTime < burnDuration)
      {
         yield return new WaitForSeconds(1f);
         ApplyDamage(burnDamagePerSecond);
         burnTime += 1f;
      }
      burnEffectParticles.Stop();
      burnEffectParticles.gameObject.SetActive(false);
      immuneStateEndTime = Time.time + immuneStateDuration;
   }


    public void ApplyChainEffect(int damage, float chainRadius, float chainDelay)
    {
        chainEffectParticles.gameObject.SetActive(true);
        chainEffectParticles.Play(); // Play chain effect particles on the current zombie

        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, chainRadius, zombieLayer);
        foreach (Collider zombie in nearbyZombies)
        {
            ZombieBehavior zombieBehavior = zombie.GetComponent<ZombieBehavior>();
            if (zombieBehavior != null && zombieBehavior != this)
            {
                zombieBehavior.PlayChainEffectParticles();
                StartCoroutine(ChainEffectCoroutine(zombieBehavior, damage, chainDelay));
            }
        }
    }


    private IEnumerator ChainEffectCoroutine(ZombieBehavior target, int damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (target != null)
        {
            target.ApplyDamage(damage);
            target.PlayChainEffectParticles(); // Play chain effect particles on the target zombie
            target.StopChainEffectParticles();
        }
    }

    public void PlayChainEffectParticles()
    {
        if (chainEffectParticles != null)
        {
            chainEffectParticles.gameObject.SetActive(true);
            chainEffectParticles.Play();
        }
    }

    public void StopChainEffectParticles()
    {
        chainEffectParticles.Stop();
        StartCoroutine(DeactivateChainEffectParticles());
    }

    private IEnumerator DeactivateChainEffectParticles()
    {
        yield return new WaitForSeconds(chainEffectParticles.main.duration);
        chainEffectParticles.gameObject.SetActive(false);
    }

    public void PlayEffectParticles(Projectile.ProjectileEffect effect)
    {
        if (effect == Projectile.ProjectileEffect.AOE)
        {
            aoeEffectParticles.gameObject.SetActive(true);
            aoeEffectParticles.Play();
        }
    }




    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (zombieValue > 0)
        {
            int valueDecrease = Mathf.Min(zombieValue, damage);
            zombieValue -= valueDecrease;
            gameManager.AddCurrency(valueDecrease);
        }
        if (health <= 0)
        {
            DestroyZombie();
        }
        else
        {
           bleedParticles.Play();
        }
    }
    private void DestroyZombie()
    {
        // Play death animation
        if (animator == null) return;
        if (animator != null)
        {
            animator.SetTrigger("Death");
            
        }
        // Delay the destruction of the game object
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("DestroyGameObject", delay);
        isDead = true;
    }

    private void DestroyGameObject()
    {
        GameManager.activeZombies.Remove(this);
        GameManager.instance.ActiveZombiesCount = GameManager.activeZombies.Count; // Update the property
        Destroy(gameObject);
    }
    


}
