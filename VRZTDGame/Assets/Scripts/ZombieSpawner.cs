using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> normalWavePrefabs;
    public List<GameObject> eliteWavePrefabs;
    public List<GameObject> bossWavePrefabs;
    public Waypoint[] waypoints;
    public float spawnDelay = 1.0f;
    public GameObject bossZombiePrefab;
    public int maxNormalWaveZombies;
    public int maxEliteWaveZombies;
    public int maxBossWaveZombies;

    public int normalWaveZombiesSpawned = 0;
    public int eliteWaveZombiesSpawned = 0;
    public int BossWaveZombiesSpawned = 0;
    public IEnumerator SpawnNormalWave()
    {
        int zombiesToSpawn = maxNormalWaveZombies;
        int spawnedZombies = 0;

        while (spawnedZombies < zombiesToSpawn)
        {
            // Spawn the new zombie prefab
            GameObject prefab = normalWavePrefabs[Random.Range(0, normalWavePrefabs.Count)];

            SpawnZombie(prefab);
            Debug.Log($"Spawned zombie {spawnedZombies + 1} of {zombiesToSpawn}.");
            spawnedZombies++;
            normalWaveZombiesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public IEnumerator SpawnEliteWave()
    {
        int zombiesToSpawn = maxEliteWaveZombies;
        int spawnedZombies = 0;

        while (spawnedZombies < zombiesToSpawn)
        {
            // Spawn the new zombie prefab
            GameObject prefab = eliteWavePrefabs[Random.Range(0, eliteWavePrefabs.Count)];

            SpawnZombie(prefab);
            Debug.Log($"Spawned zombie {spawnedZombies + 1} of {zombiesToSpawn}.");
            spawnedZombies++;
            eliteWaveZombiesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }



    public IEnumerator SpawnBossWave()
    {

        int zombiesToSpawn = maxBossWaveZombies;
        int spawnedZombies = 0;

        while (spawnedZombies < zombiesToSpawn)
        {
            // Spawn the new zombie prefab
            GameObject prefab = bossWavePrefabs[Random.Range(0, bossWavePrefabs.Count)];
            if (prefab.tag == "BossZombie")
            {
                // Spawn the boss zombie separately from the other zombies
                SpawnZombie(prefab);
                Debug.Log("Spawned boss zombie.");
                bossWavePrefabs.Remove(prefab);
            }
            else
            {
                SpawnZombie(prefab);
                Debug.Log($"Spawned zombie {spawnedZombies + 1} of {zombiesToSpawn}.");
                spawnedZombies++;
                BossWaveZombiesSpawned++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }






    private void SpawnZombie(GameObject prefab)
    {
        GameObject spawnedZombie = Instantiate(prefab, transform.position, Quaternion.identity);
        ZombieBehavior zombieBehavior = spawnedZombie.GetComponent<ZombieBehavior>();

        if (zombieBehavior != null)
        {
            zombieBehavior.waypoints = waypoints;
            GameManager.activeZombies.Add(zombieBehavior);
        }
        else
        {
            Debug.LogError("Zombie prefab is missing ZombieBehavior script.");
        }
    }
}
