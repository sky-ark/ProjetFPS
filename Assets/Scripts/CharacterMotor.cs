using UnityEngine;

//Empêche la suppression du CharacterController 
[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour {

    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private float Speed = 0.2f;
    [SerializeField] private float RunSpeed = 4f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private Vector3 moveVelocity;
    [SerializeField] private Vector3 jumpVelocity;
    
    private CharacterController _characterController;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;

    public AnimatorController animator;
    
    private void Awake() {
    
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        //déplacements gauche/droite devant/derriere
        float moveZ = Input.GetAxisRaw("Horizontal");
        float moveX = Input.GetAxisRaw("Vertical");
        moveVelocity = (moveZ * transform.right) + (moveX * transform.forward);
        
        //Gestion du Sprint 
        bool runKey = Input.GetKey(KeyCode.LeftShift);
        if (runKey) {
            if (moveX >= 0)
            {
                moveVelocity *= RunSpeed;
            }
        }
        
        // Déplacement via characterController
        _characterController.Move(moveVelocity * (Time.deltaTime * Speed));
        
        // Gestion de la Gravité
        jumpVelocity.y += gravityValue * Time.deltaTime;
        if (groundDetection.IsCollided && jumpVelocity.y < 0)
        {
            jumpVelocity.y = 0f;
        }

        // Gestion du Saut
        //Debug.Log("Grounded : " + _characterController.isGrounded);
        if (Input.GetButtonDown("Jump") && groundDetection.IsCollided) {
            jumpVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        if (Input.GetButtonDown("Fire1") ){
            animator.SetTrigger("shoot");
        }
        // Saut via characterController

        _characterController.Move(jumpVelocity * Time.deltaTime);

    }
    
}

// à faire : 
// - Bloquer la rotation maximal de la caméra 
// - Faire en sorte que le joueur touche le sol ( pourquoi ne tombe t'il pas au sol actuellement ? ) 
// - Vérifier la fonction de saut. 
// - Comprendre pourquoi le sprint ne fonctionne pas 