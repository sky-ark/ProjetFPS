using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Spec de l'arme
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    //Gestion Munitions
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;
    [SerializeField] private int _currentAmmoInClip;
    [SerializeField] private int _ammoInReserve;
    //Appel de la caméra pour le raycast
    public Camera fpsCam;
    //Ref pour le flash du tir
    public ParticleSystem muzzleFlash;
    //Ref pour particle de l'impact
    public GameObject impactEffect;
    private float nextTimeToFire = 0f;
 
    
    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && _currentAmmoInClip > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            _currentAmmoInClip--;
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
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            //Si on touche une cible
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            //Recul de la cible si elle a un rigidbody.
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            //Fait apparaitre un gameObject de particule à l'endroit de l'impact avec une rotation par rapport à la normal de la cible.
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destruction du GO de l'impact
            Destroy(impactGO, 2f);
        }
            
    }
}
