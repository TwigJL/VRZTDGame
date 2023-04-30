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

    public int waveCT;
    public static List<ZombieBehavior> activeZombies = new List<ZombieBehavior>();
    public int currentWave = 1;
    private bool spawnNewWave = false;
    private int _activeZombiesCount;
    public int ActiveZombiesCount
    {
        get => _activeZombiesCount;
        set
        {
            if (_activeZombiesCount != value)
            {
                _activeZombiesCount = value;
                Debug.Log("Zombies Alive:" + _activeZombiesCount);
            }
        }
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
        StartCoroutine(zombieSpawner.SpawnNormalWave());

        waveCT++;
        Debug.Log("Spawning Normal Wave");
    }
    private IEnumerator Countdown(int seconds, System.Action onFinish)
    {
        for (int i = seconds; i > 0; i--)
        {
            Debug.Log($"Next wave in {i} seconds...");
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Wave Count At " + waveCT);
        onFinish?.Invoke();
    }

    private void Update()
    {
        if (GameManager.activeZombies.Count == 0 && !spawnNewWave)
        {
            spawnNewWave = true;

            if (currentWave == waveCT)
            {
                waveCT++;
                
                System.Action spawnWave = null;

                if (waveCT % 5 == 0)
                {
                    spawnWave = () => StartCoroutine(zombieSpawner.SpawnBossWave());
                    Debug.Log("Spawning Boss Wave");
                }
                else if (waveCT % 3 == 0)
                {
                    spawnWave = () => StartCoroutine(zombieSpawner.SpawnEliteWave());
                    Debug.Log("Spawning Elite Wave");
                }
                else
                {
                    spawnWave = () => StartCoroutine(zombieSpawner.SpawnNormalWave());
                    Debug.Log("Spawning Normal Wave");
                }

                StartCoroutine(Countdown(10, () =>
                {
                    spawnWave();
                    currentWave++;
                    spawnNewWave = false;
                }));
            }
        }
    }


}
