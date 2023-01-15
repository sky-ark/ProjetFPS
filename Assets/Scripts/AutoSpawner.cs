using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
    public Transform Player;
    //à partir de quelle distance du joueur à la zone de spawn, on spawn l'ennemi
    public int SpawnDistance;
    //Appel du prefab de l'ennemi à spawn
    public GameObject PrefabToSpawn;

    public float Distance;
    //vitesse de spawn
    public float SpawnRate;

    private float NextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si la distance player ennemi est plus élevée, alors commencer à spawn ( permet de faire en sorte d'alterner entre les zones de spawn )
        Distance = Vector3.Distance(transform.position, Player.position);
        if (Distance > SpawnDistance)
        {
            //Si le temps depuis le début du Jeu.
            if (Time.time > NextSpawn)
            {
                NextSpawn = Time.time + SpawnRate;
                Instantiate(PrefabToSpawn, transform.position, Quaternion.identity);  
            }
        }
    }
}
