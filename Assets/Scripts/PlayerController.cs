using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // SerializeField permet d'afficher dans l'inspecteur une variable private
    [SerializeField] private float Speed;

    private PlayerMotor Motor;
    // Start is called before the first frame update
    void Start()
    {
        Motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculer la vitesse du mouvement de notre joueur
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * Speed;

        Motor.Move(velocity);
    }
}
