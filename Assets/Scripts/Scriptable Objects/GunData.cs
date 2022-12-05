using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// créé un menu d'assets
[CreateAssetMenu(fileName = "Gun" , menuName = "Weapon/Gun")]
public class GunData: ScriptableObject 
{
    // Start is called before the first frame update
    public new string name;
    public float damage;
    public float maxDistance;
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector] public bool reloading;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
