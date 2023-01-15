using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Ref à la vie du joueur
    public float restartDelay = 5f;            // Temps avant le restart du level


    Animator anim;                          // Ref à l'animator
    float restartTimer;                     // Timer pour le restart


    void Awake ()
    {
        // Set up the reference.
        anim = GetComponent <Animator> ();
    }


    void Update ()
    {
        // If the player has run out of health...
        if(playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the game is over.
            anim.SetTrigger ("GameOver");

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if(restartTimer >= restartDelay)
            {
                // .. then reload the currently loaded level.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}