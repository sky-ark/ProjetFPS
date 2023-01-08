using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMaster : MonoBehaviour
{
    public GameObject player;

    public float distance;

    public bool isAngered;

    public NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul de la Distance entre l'enemi et le joueur 
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        // Si la distance est inférieur ou égale à 5 l'enemi attaque
        if(distance <= 5) isAngered = true;
        
        else if (distance > 5) isAngered = false;
        if (isAngered)
        {
            _agent.SetDestination(player.transform.position);
            _agent.isStopped = false;
        }
        else _agent.isStopped = true; 
    }
}
