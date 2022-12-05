using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gun Settings")] public float damage = 10f;
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapicity = 270;
    [SerializeField] private bool _canShoot;
    [SerializeField] private int _currentAmmoInClip;
    [SerializeField] private int _ammoInReserve;
    //Visée
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10f;
    //Balancement de l'arme
    public CameraHandler cameraHandler; 

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapicity;
        _canShoot = true;
    }

    private void Update()
    {
        //Appel de la fonction de visée
        DetermineAim();
        //Tir
        if (Input.GetButton("Fire1") && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        
        //Rechargement si moins de balles dans le chargeur que sa capicité, et balles en réserve supérieures à zéro
        else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            //Calcul du nombre de balles nécessaires pour compléter le chargeur
            int amountNeeded = clipSize - _currentAmmoInClip;
           //Si le nombre de balles demandées est supérieur à la réserve, mettre toute la réserve dans le chargeur
            if (amountNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve = 0;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= amountNeeded;
            }
        }
        // Balancement de l'arme 
    }
    
 
// function de visée ( déplacement du model au centre de l'écran quand clic droit ) 
    void DetermineAim()
    {
        Vector3 target = normalLocalPosition;
        if (Input.GetButton("Fire2")) target = aimingLocalPosition;
        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPosition;
    }
    
    
    IEnumerator ShootGun()
    {
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
}

