using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
    

    //Appel du prefab de l'ennemi à spawn
    public GameObject Ennemy;

    
    //vitesse de spawn
    public float SpawnRate;

    public float OverallSpawnRate;

    public static int DeathNumber;

    public AnimationCurve SpawnFunction; // création d'une courbe pour la vitesse de spawn 
    //SpawnPosition
    public List<GameObject> SpawnPosition;
    //Temps
    private float _currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        OverallSpawnRate = SpawnFunction.Evaluate(DeathNumber);
        if (_currentTime > OverallSpawnRate)
        {
            SpawnMob();
            _currentTime = 0;
        }
    }

    void SpawnMob()
    {
        GameObject spawn = SpawnPosition[Random.Range(0, SpawnPosition.Count)];
        Instantiate(Ennemy, spawn.transform.position, Quaternion.identity);
    }
}
