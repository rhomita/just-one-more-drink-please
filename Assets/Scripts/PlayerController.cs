using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private LegsMovement legsMovement;
    
    private CharacterController controller;
    
    // Jump
    private float groundDistance = 0.4f;
    private float gravity = -9.8f;
    private Vector3 gravityDirection;
    private bool isGrounded;
    
    // Running
    private static float MAX_ENERGY = 100f;
    private static float ENERGY_CHARGE_SPEED = 8f;
    private float energy = MAX_ENERGY;
    private bool isRunning = false;
    private Vector3 runningCenter;

    private float pushForce = 1f;
    
    void Awake()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    void Start()
    {
        runningCenter = new Vector3(0, 0.1f, 0);
    }
    
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        // Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            energy -= ENERGY_CHARGE_SPEED * 2f * Time.deltaTime;
            isRunning = energy > 1f;
        } else
        {
            isRunning = false;
            energy += ENERGY_CHARGE_SPEED * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, MAX_ENERGY);
        }

        legsMovement.SetRunning(isRunning);
        legsMovement.IsMoving = input.sqrMagnitude > 0;
        
        if (legsMovement.IsMoving)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            
            controller.center = isRunning ? runningCenter : Vector3.zero;

            Vector3 movement = transform.forward * (Time.deltaTime * speed * (isRunning ? 2 : 1));
            controller.Move(movement);
        }
        

        if (isGrounded && gravityDirection.y < 0)
        {
            gravityDirection.y = -2f;
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        /*
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        gravityDirection.y += gravity * Time.deltaTime;
        controller.Move(gravityDirection * Time.deltaTime);
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer != 10) return;
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * (legsMovement.IsRunning ? pushForce * 1.8f : pushForce);  
    }
}
