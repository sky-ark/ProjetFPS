using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool IsCollided;
    
    private void OnTriggerEnter(Collider other) {
        IsCollided = true;
    }

    private void OnTriggerExit(Collider other) {
        IsCollided = false;
    } 
    
}
