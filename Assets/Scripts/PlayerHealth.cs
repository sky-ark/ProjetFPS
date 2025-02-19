using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // Vie de départ
    public int currentHealth;                                   // Vie actuelle
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
   // public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    //Animator anim;                                              // Reference to the Animator component.
    public AudioSource audioSource;                                    // Reference to the AudioSource component.
    public AudioClip HurtSound;
    public AudioClip DyingSound; 
    CharacterController characterController;                              // Ref au script du characterController
    Gun gun;                              // Reference au script du shoot du gun
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake ()
    {
        // Setting up the references.
       // anim = GetComponent <Animator> ();
        audioSource = GetComponent <AudioSource> ();
        characterController = GetComponent <CharacterController> ();
        gun = GetComponentInChildren <Gun> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update ()
    {
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        audioSource.PlayOneShot(HurtSound);

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //gun.DisableEffects ();

        // Tell the animator that the player is dead.
        // anim.SetTrigger ("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        audioSource.PlayOneShot(DyingSound);
        //playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        characterController.enabled = false;
        gun.enabled = false;
    }        
}
