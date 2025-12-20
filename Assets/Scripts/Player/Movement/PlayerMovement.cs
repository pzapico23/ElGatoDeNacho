using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float maxSpeed = 4;
    [SerializeField]
    private float initialSpeed = 2;
    [SerializeField]
    private float jumpHeight = 10;
    [SerializeField]
    private float fallSpeed = 2;
    private float currentSpeed;
    private bool isJumping = false;
    private bool isJumpHeld = false;
    private bool isGrounded = false;
    [SerializeField]
    private Vector2 groundCheckBox;
    [SerializeField]
    private float castDistance;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        checkGround();
        Move();
        Jump();
    }

    internal void OnMove(Vector2 vector2)
    {
        float auxSpeed = vector2.x * (maxSpeed - initialSpeed);
        this.currentSpeed = auxSpeed != 0 ? initialSpeed + auxSpeed : 0;
    }
    internal void OnJump(float v)
    {
        isJumpHeld = v != 0;
        Debug.Log(v);
    }
    private void Move()
    {
        rb.linearVelocityX = currentSpeed;
    }
    private void Jump()
    {
        if(isJumpHeld && isGrounded)
        {
            rb.linearVelocityY = jumpHeight;
            isJumping = true;
        }
        if (!isGrounded && isJumping)
        {
            if(rb.linearVelocityY > 0)
            {
                rb.linearVelocityY = rb.linearVelocityY - fallSpeed * Time.deltaTime;
            }
        }

    }


    private void checkGround()
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
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBox);
    }

    
}
