using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//Appel de la lib pour l'UI
using UnityEngine.UI;

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
    //Affichage Munitions
    public TMP_Text ammoDisplay;
    //temps de rechargement
    private bool isReloading = false;
    public float reloadingTime;
    //Affichage du Rechargement
    public TMP_Text reloadingDisplay;
    //Animation du  Rechargement
    public Animator animator; 
    //Appel de la caméra pour le raycast
    public Camera fpsCam;
    //Ref pour le flash du tir
    public ParticleSystem muzzleFlash;
    //Ref pour particle de l'impact
    public GameObject impactEffect;
    private float nextTimeToFire = 0f;
    //Ref Audio 
    public AudioSource audioSource;
    public AudioClip ShootSound;
    public AudioClip ReloadSound;
    public AudioClip PickUpSound;
   

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        //Appel Component Audio
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;
        //Transformation des variables de munitions en chaines de charactères pour l'affichage
        ammoDisplay.text = _currentAmmoInClip.ToString() + " | " + _ammoInReserve.ToString();
        //Temps entre 2 balles tirées.
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && _currentAmmoInClip > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            _currentAmmoInClip--;
        }
        //Rechargement si moins de balles dans le chargeur que sa capicité, et balles en réserve supérieures à zéro
        else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            StartCoroutine(Reload());
            return;
        }

       
            
        
    }

    void Shoot()
    {
        muzzleFlash.Play();
        audioSource.PlayOneShot(ShootSound); 
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

    public void RefillAmo()
    {
        audioSource.PlayOneShot(PickUpSound);
        _ammoInReserve = reservedAmmoCapacity;
    }
    
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("isreloading");
        //affichage de "reloading"
        reloadingDisplay.text = " Reloading ";
        //Son Rechargement
        audioSource.PlayOneShot(ReloadSound);
        //Animation du Rechargement 
        animator.SetBool("Reloading", true);
        
        //temps de rechargement
        
        yield return new WaitForSeconds(reloadingTime);
        //fin animation rechargement et "reloading"
        animator.SetBool("Reloading", false);
        reloadingDisplay.text = " ";

        //Calcul du nombre de balles nécessaires pour compléter le chargeur
        int amountNeeded = clipSize - _currentAmmoInClip;
        //Si le nombre de balles demandées est supérieur à la réserve, mettre toute la réserve dans le chargeur
        if (amountNeeded >= _ammoInReserve)
        {
            _currentAmmoInClip += _ammoInReserve;
            _ammoInReserve = 0;
            isReloading = false;
        }
        else
        {
            _currentAmmoInClip = clipSize;
            _ammoInReserve -= amountNeeded;
            isReloading = false;
        }

   

    }
}
