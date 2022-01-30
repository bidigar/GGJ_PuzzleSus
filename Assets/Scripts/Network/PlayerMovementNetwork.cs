using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementNetwork : NetworkBehaviour, IMovement
{
    private CharacterController controller;
    [SerializeField] float speed;
    float gravity = -9.81f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    protected bool m_canMove = true;
    
    public bool CanMove
    {
        get => m_canMove;
        set => m_canMove = value;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(!isLocalPlayer || !CanMove) return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        GroundCheck();
        CharacterMovement(x, z);
        CharacterGravity();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }
    }

    void CharacterMovement(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void CharacterGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
