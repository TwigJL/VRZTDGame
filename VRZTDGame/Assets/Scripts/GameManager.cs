using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
   public List<ZombieSpawner> zombieSpawners;
   public GameObject scoreUI;
   public GameObject KeyboardToggle;
   public int maxNormalWaveZombies = 10;
    public int maxEliteWaveZombies = 20;
    public int maxBossWaveZombies = 30;
    public AudioSource audioSource;
    public AudioClip normalWaveClip;
    public AudioClip eliteWaveClip;
    public AudioClip bossWaveClip;
    public int waveCT;
    public static List<ZombieBehavior> activeZombies = new List<ZombieBehavior>();
    public bool spawnNewWave = false;
    public int currency = 800;
    public int _activeZombiesCount;
    public Shop shop;
    public int health = 150;
    public bool isPaused = false;
   public int ActiveZombiesCount
    {
        get => _activeZombiesCount;
        set
        {
            if (_activeZombiesCount != value)
            {
                _activeZombiesCount = value;
                Debug.Log("Zombies Alive:" + _activeZombiesCount + "\n" + new System.Diagnostics.StackTrace());
            }
        }
    }
   public void SetPauseState(bool paused)
   {
      isPaused = paused;
   }
    public void AddCurrency(int amount)
    {
        currency += amount;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
   private void Start()
   {
      // Set the max zombie counts in all the zombie spawners
      foreach (var spawner in zombieSpawners)
      {
         spawner.maxNormalWaveZombies = maxNormalWaveZombies;
         spawner.maxEliteWaveZombies = maxEliteWaveZombies;
         spawner.maxBossWaveZombies = maxBossWaveZombies;
      }

      // Spawn a normal wave
      ActiveZombiesCount = 10 * zombieSpawners.Count;

      audioSource.PlayOneShot(normalWaveClip);
      StartCoroutine(StartFirstWaveWithDelay(10f));
      scoreUI.SetActive(false);
      KeyboardToggle.SetActive(false);
   }
   private IEnumerator StartFirstWaveWithDelay(float delaySeconds)
   {
      audioSource.PlayOneShot(normalWaveClip);
      Debug.Log($"First wave will start in {delaySeconds} seconds...");
      yield return new WaitForSeconds(delaySeconds);

      // Call SpawnNormalWave() on all spawners
      foreach (var spawner in zombieSpawners)
      {
         StartCoroutine(spawner.SpawnNormalWave());
      }

      waveCT++;
      spawnNewWave = true;
   }

   private IEnumerator StartNormalWaveWithDelay(float delaySeconds)
   {
      audioSource.PlayOneShot(normalWaveClip);
      Debug.Log($"Normal wave will start in {delaySeconds} seconds...");
      yield return new WaitForSeconds(delaySeconds);

      // Call SpawnNormalWave() on all spawners
      foreach (var spawner in zombieSpawners)
      {
         StartCoroutine(spawner.SpawnNormalWave());
      }

      waveCT++;
      spawnNewWave = true;
   }

   private IEnumerator StartEliteWaveWithDelay(float delaySeconds)
   {
      audioSource.PlayOneShot(eliteWaveClip);
      Debug.Log($"Elite wave will start in {delaySeconds} seconds...");
      yield return new WaitForSeconds(delaySeconds);

      // Call SpawnEliteWave() on all spawners
      foreach (var spawner in zombieSpawners)
      {
         StartCoroutine(spawner.SpawnEliteWave());
      }

      waveCT++;
      spawnNewWave = true;
   }

   private IEnumerator StartBossWaveWithDelay(float delaySeconds)
   {
      audioSource.PlayOneShot(bossWaveClip);
      Debug.Log($"Boss wave will start in {delaySeconds} seconds...");
      yield return new WaitForSeconds(delaySeconds);

      // Call SpawnBossWave() on all spawners
      foreach (var spawner in zombieSpawners)
      {
         StartCoroutine(spawner.SpawnBossWave());
      }

      waveCT++;
      spawnNewWave = true;
   }
   


   private void Update()
    {
        if (GameManager.activeZombies.Count == 0 && spawnNewWave)
        {
            spawnNewWave = false;
            if (waveCT % 5 == 0)
            {
                Debug.Log("Spawning Boss Wave");
                StartCoroutine(StartBossWaveWithDelay(3f));
            }
            else if (waveCT % 3 == 0)
            {
                Debug.Log("Spawning Elite Wave");
                StartCoroutine(StartEliteWaveWithDelay(5f));

            }
            else
            {
                Debug.Log("Spawning Normal Wave");
                StartCoroutine(StartNormalWaveWithDelay(10f));

            }
        }
      if (health <= 0)
      {
         health = 0; // Ensure that health doesn't go below 0
         if (!scoreUI.activeSelf)
         {
            scoreUI.SetActive(true);
            KeyboardToggle.SetActive(true);
            isPaused = true;
         }
      }
    }


}
