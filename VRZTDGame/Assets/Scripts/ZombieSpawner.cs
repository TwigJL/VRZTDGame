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
   private bool BossSpawned = false;
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
         if (!BossSpawned)
         {
            // Spawn the boss zombie separately from the other zombies
            SpawnZombie(bossZombiePrefab);
            BossSpawned = true;
         }
         else
         {
            SpawnZombie(prefab);

            spawnedZombies++;
            BossWaveZombiesSpawned++;
         }
         yield return new WaitForSeconds(spawnDelay);
      }
      BossSpawned = false;
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
