using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public GameObject enemy;
    private Wave currentWave;
    private int currentWaveNumber;
    private int _enemiesRemainingToSpawn;
    private float _nextSpawnTime;
    
    [System.Serializable]
    public class Wave
    {
        public int ennemyCount;
        public float timeBetweenSpawns;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];
        _enemiesRemainingToSpawn = currentWave.ennemyCount;
    }
}
