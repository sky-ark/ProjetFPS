using UnityEngine;

public class AmmoPickUp : MonoBehaviour {
    
    public Gun Gun;
    public GameObject Player; 
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Debug.Log("Refill");
            Gun.RefillAmo();
        }
    }
}
