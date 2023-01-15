using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMaster : MonoBehaviour
{
    public Transform player;

    public float distance;
    public float triggerDistance;

    public bool isAngered;

    public NavMeshAgent _agent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //assigne le joueur a suivre par les zombies
        player = GameObject.Find("PlayerCC").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul de la Distance entre l'enemi et le joueur 
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        // Si la distance est inférieur ou égale à 5 l'enemi attaque
        if(distance <= triggerDistance) isAngered = true;
        
        else if (distance > triggerDistance) isAngered = false;
        if (isAngered)
        {
            _agent.SetDestination(player.transform.position);
            _agent.isStopped = false;
        }
        else _agent.isStopped = true; 
    }
}
