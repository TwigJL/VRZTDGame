using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Waypoint[] waypoints;
    public int numberOfZombies = 10;
    public float spawnDelay = 1.0f;

    private IEnumerator Start()
    {
        for (int i = 0; i < numberOfZombies; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnZombie()
    {
        GameObject spawnedZombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        ZombieBehavior zombieBehavior = spawnedZombie.GetComponent<ZombieBehavior>();

        if (zombieBehavior != null)
        {
            zombieBehavior.waypoints = waypoints;
        }
        else
        {
            Debug.LogError("Zombie prefab is missing ZombieBehavior script.");
        }
    }
}
