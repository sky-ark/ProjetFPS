using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    [SerializeField] private float sensitivity =1f;
    //Suppression du Curseur, blocage de ce dernier au milieu.
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void LateUpdate()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * sensitivity;
        mouseY -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90f, 45f);
        // Quarternion gère les angles de rotations
        Quaternion cameraRotation = Quaternion.Euler(mouseY, 0f, 0f);
        Quaternion playerRotation = Quaternion.Euler(0f, mouseX, 0f);
        // Rotate du joueur entier pour les rotations latérations, de la caméra uniquement pour les rotations verticales
        transform.localRotation = cameraRotation;
        transform.parent.localRotation = playerRotation;
    }
}
