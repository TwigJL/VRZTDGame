using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public Waypoint[] waypoints;
    private Animator animator;
    private int currentWaypoint = 0;
    private float chooseCooldown = 0.0f;
    public float chooseDelay = 1.0f;
    public float slowDuration = 2f;
    public float freezeDuration = 3f;
    public float burnDuration = 5f;
    public int burnDamagePerSecond = 10;
    public int maxHealth = 100;
    private int health;
    private float normalSpeed;
    private Coroutine slowCoroutine;
    private Coroutine freezeCoroutine;
    private Coroutine burnCoroutine;
    public LayerMask zombieLayer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        normalSpeed = speed;
        health = maxHealth;
        animator.SetFloat("Speed", speed);
        zombieLayer = LayerMask.GetMask("Zombie");
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

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        if (chooseCooldown > 0.0f)
        {
            chooseCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentWaypoint < waypoints.Length && waypoints[currentWaypoint] != null &&
        other.gameObject == waypoints[currentWaypoint].gameObject && chooseCooldown <= 0.0f)
        {
            ChooseNextWaypoint();
            chooseCooldown = chooseDelay;
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
    }
    public void ApplySlow()
    {
        if (slowCoroutine != null) StopCoroutine(slowCoroutine);
        slowCoroutine = StartCoroutine(SlowEffect());
    }

    private IEnumerator SlowEffect()
    {
        animator.SetFloat("Speed", normalSpeed / 2f);
        yield return new WaitForSeconds(slowDuration);
        animator.SetFloat("Speed", normalSpeed);
    }

    public void ApplyFreeze()
    {
        if (freezeCoroutine != null) StopCoroutine(freezeCoroutine);
        freezeCoroutine = StartCoroutine(FreezeEffect());
    }

    private IEnumerator FreezeEffect()
    {
        animator.SetFloat("Speed", 0f);
        yield return new WaitForSeconds(freezeDuration);
        animator.SetFloat("Speed", normalSpeed);
    }

    public void ApplyBurn()
    {
        if (burnCoroutine != null) StopCoroutine(burnCoroutine);
        burnCoroutine = StartCoroutine(BurnEffect());
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
    }
    public void ApplyChainEffect(int damage, float chainRadius, float chainDelay)
    {
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, chainRadius, zombieLayer);
        foreach (Collider zombie in nearbyZombies)
        {
            ZombieBehavior zombieBehavior = zombie.GetComponent<ZombieBehavior>();
            if (zombieBehavior != null && zombieBehavior != this)
            {
                StartCoroutine(ChainEffectCoroutine(zombieBehavior, damage, chainDelay));
            }
        }
    }

    private IEnumerator ChainEffectCoroutine(ZombieBehavior target, int damage, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.ApplyDamage(damage);
    }



    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyZombie();
        }
    }
    private void DestroyZombie()
    {
        // Play death animation
        animator.SetTrigger("Death");

        // Delay the destruction of the game object
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("DestroyGameObject", delay);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }


}
