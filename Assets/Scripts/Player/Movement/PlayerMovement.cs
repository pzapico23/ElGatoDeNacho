using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 4;
    [SerializeField]
    private float initialSpeed = 2;
    [SerializeField]
    private float jumpHeight = 5;
    [SerializeField]
    private Vector2 groundCheckBox;
    [SerializeField]
    private float castDistance;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float coyoteTime = 2;


    private float currentSpeed;
    private bool isJumping = false;
    private bool isJumpHeld = false;
    private bool isGrounded = false;
    private float currentCoyoteTime = 0;
    private bool wasGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        CheckGround();
        Jump();
        Fall();
        Move();
       
        wasGrounded = isGrounded;
    }

    private void Fall()
    {
        if (wasGrounded && !isGrounded && !isJumping)
        {
            currentCoyoteTime += Time.deltaTime;
        }
        if (isGrounded)
        {
            currentCoyoteTime = 0;
            isJumping = false;
        }
        
    }

    internal void OnMove(Vector2 vector2)
    {
        this.currentSpeed = vector2.x != 0 ? vector2.x * initialSpeed : 0;
    }
    internal void OnJump(float v)
    {
        isJumpHeld = v != 0;
    }
    private void Move()
    {
        rb.linearVelocityX = currentSpeed;
    }
    private void Jump()
    {
        if (isJumpHeld && (isGrounded || (currentCoyoteTime < coyoteTime && currentCoyoteTime != 0)))
        {
            rb.AddForceY(jumpHeight);
            isJumping = true;
            currentCoyoteTime = 0;
            Debug.Log("Cy:" + currentCoyoteTime);
        }
    }

    private void CheckGround()
    {
        if(Physics2D.BoxCast(transform.position, groundCheckBox, 0, -transform.up, castDistance,groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBox);
    }

    
}
