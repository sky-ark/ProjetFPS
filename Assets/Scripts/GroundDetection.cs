using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool IsCollided;
    
    private void OnTriggerStay(Collider other) {
        IsCollided = true;
    }

    private void OnTriggerExit(Collider other) {
        IsCollided = false;
    } 
    
}
