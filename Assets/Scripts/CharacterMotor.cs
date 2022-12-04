using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharacterMotor : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController mController;
    [SerializeField] private float Speed = 0.2f;
    [SerializeField] private float RunSpeed = 4f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    [SerializeField]private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    
    private void Awake()
    {
        mController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
    
        //déplacements gauche/droite devant/derriere
        float moveZ = Input.GetAxisRaw("Horizontal");
        float moveX = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = (moveZ * transform.right) + (moveX * transform.forward);
        mController.Move(moveDir * Speed);
        //Gestion du Sprint -- ne fonctionne pas?
        bool runKey = Input.GetKey(KeyCode.LeftShift);
        if (runKey)
        {
            Debug.Log("Is running = " + runKey);
            moveDir *= RunSpeed;
        }


    }

    private void Update()
    {
        // Gestion du saut 
        groundedPlayer = mController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {

            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue; 
    }
}

// à faire : 
// - Bloquer la rotation maximal de la caméra 
// - Faire en sorte que le joueur touche le sol ( pourquoi ne tombe t'il pas au sol actuellement ? ) 
// - Vérifier la fonction de saut. 
// - Comprendre pourquoi le sprint ne fonctionne pas 