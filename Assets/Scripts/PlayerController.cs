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
 
    private Transform cam;
    private CharacterController controller;
    
    // Jump
    private float groundDistance = 0.4f;
    private float gravity = -9.8f;
    private Vector3 gravityDirection;
    private bool isGrounded;
    
    // Running
    private static float MAX_ENERGY = 100f;
    private static float ENERGY_CHARGE_SPEED = 3f;
    private float energy = MAX_ENERGY;
    private bool isRunning = false;
    private bool isCrouching = false;
    private Vector3 runningCenter;
    private Vector3 crouchingCenter;

    private float pushForce = 0.8f;
    
    void Awake()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    void Start()
    {
        cam = GameManager.instance.Camera;
        runningCenter = new Vector3(0, 0.1f, 0);
        crouchingCenter = new Vector3(0, 0.3f, 0);
    }
    
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        legsMovement.IsMoving = input.sqrMagnitude > 0;
        
        // Running
        if (legsMovement.IsMoving && Input.GetKey(KeyCode.LeftShift))
        {
            energy -= ENERGY_CHARGE_SPEED * 2f * Time.deltaTime;
            isRunning = energy > 1f;
        } else
        {
            isRunning = false;
            energy += ENERGY_CHARGE_SPEED * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, MAX_ENERGY);
        }

        isCrouching = !isRunning && Input.GetKey(KeyCode.C);
        
        legsMovement.SetRunning(isRunning);
        controller.center = isRunning ? runningCenter : isCrouching ? crouchingCenter : Vector3.zero;
        
        if (legsMovement.IsMoving)
        {
            Vector3 forward = cam.forward.normalized;
            Vector3 right = cam.right.normalized;
            forward.y = 0;
            right.y = 0;
            Vector3 lookRotation = forward * input.z + right * input.x;
            
            Quaternion rotation = Quaternion.LookRotation(lookRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            float extraSpeed = (isRunning ? 2 : isCrouching ? 0.5f : 1);
            Vector3 movement = transform.forward * (Time.deltaTime * speed * extraSpeed);
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
