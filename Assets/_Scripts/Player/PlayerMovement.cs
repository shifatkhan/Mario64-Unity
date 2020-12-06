using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController controller;
    [SerializeField, Min(0)] private float speed = 5f;
    [SerializeField, Min(0)] private float rotationSpeed = 10f;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField, Min(0)] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField, Min(0)] private float jumpHeight = 2f;

    public Vector3 movement { get; private set; }
    private Vector3 gravitationalForce;
    public bool isGrounded { get; private set; }
    public bool jumpMomentumCheck { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // check if mario is grounded
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        isGrounded = false;
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            if (!hitColliders[i].isTrigger)
            {
                isGrounded = true;
                break;
            }
        }

        // calculate movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
        movement = right * horizontal + forward * vertical;

        // check if player is trying to move
        if (movement != Vector3.zero)
        {
            // look in the direction of the movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);

            if (isGrounded)
            {
                audioSource.enabled = true;
                audioSource.loop = true;
            }
            else
            {
                audioSource.enabled = false;
                audioSource.loop = false;
            }
        }
        else
        {
            audioSource.enabled = false;
            audioSource.loop = false;
        }

        jumpMomentumCheck = jumpMomentumCheck && Input.GetButton("Jump") && !isGrounded;

        // simulate gravity
        if (isGrounded)
        {
            // mario is standing on ground
            gravitationalForce.y = gravity * Time.deltaTime;
            jumpMomentumCheck = true;
        }
        else
        {
            // mario is in the air
            if (!jumpMomentumCheck && gravitationalForce.y > 0)
                gravitationalForce.y = 0;
            else
                gravitationalForce.y += gravity * Time.deltaTime;
        }

        // jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitationalForce.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }
        // move mario
        controller.Move((movement * speed * Time.deltaTime) + (gravitationalForce * Time.deltaTime));
    }
}
