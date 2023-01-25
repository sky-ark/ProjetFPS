using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float enemyHealth = 50f; // Vie de l'ennemi
    public int scoreValue = 10; // Points pour la mort de l'ennemi. 

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        ScoreManager.score += scoreValue;
        AutoSpawner.DeathNumber++;
    }
}
