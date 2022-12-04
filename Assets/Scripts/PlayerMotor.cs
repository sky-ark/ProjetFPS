using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void FixedUpdate()
    {
        
    }

    private void PerformMovement()
    {
        if(velocity != Vector3.zero) rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
