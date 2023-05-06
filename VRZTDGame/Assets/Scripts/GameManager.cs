using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ZombieSpawner zombieSpawner;
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
        // Set the max zombie counts in the zombie spawner
        zombieSpawner.maxNormalWaveZombies = maxNormalWaveZombies;
        zombieSpawner.maxEliteWaveZombies = maxEliteWaveZombies;
        zombieSpawner.maxBossWaveZombies = maxBossWaveZombies;
        // Spawn a normal wave
        ActiveZombiesCount = 10;
        
        audioSource.PlayOneShot(normalWaveClip);
        StartCoroutine(StartFirstWaveWithDelay(10f));
    }
    private IEnumerator StartFirstWaveWithDelay(float delaySeconds)
    {
        audioSource.PlayOneShot(normalWaveClip);
        Debug.Log($"First wave will start in {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(zombieSpawner.SpawnNormalWave());
        waveCT++;
        spawnNewWave = true;
        
    }

    private IEnumerator StartNormalWaveWithDelay(float delaySeconds)
    {
        audioSource.PlayOneShot(normalWaveClip);
        Debug.Log($"Normal wave will start in {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(zombieSpawner.SpawnNormalWave());
        waveCT++;
        spawnNewWave = true;

    }

    private IEnumerator StartEliteWaveWithDelay(float delaySeconds)
    {
        audioSource.PlayOneShot(eliteWaveClip);
        Debug.Log($"Elite wave will start in {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(zombieSpawner.SpawnEliteWave());
        waveCT++;
        spawnNewWave = true;

    }

    private IEnumerator StartBossWaveWithDelay(float delaySeconds)
    {
        audioSource.PlayOneShot(bossWaveClip);
        Debug.Log($"Boss wave will start in {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(zombieSpawner.SpawnBossWave());
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
    }


}
