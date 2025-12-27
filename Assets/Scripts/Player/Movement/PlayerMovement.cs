using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float initialSpeed;
    [SerializeField]
    private float hAcceleration;
    [SerializeField]
    private float jumpStrength;
    [SerializeField]
    private float holdJumpStrength;
    [SerializeField]
    private Vector2 groundCheckBox;
    [SerializeField]
    private float castDistance;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private float inputBufferTime;


    private float currentSpeed;
    private bool isJumping = false;
    private bool jusJumped = false;
    private bool isJumHeld = false;
    private bool isGrounded = false;
    private float currentCoyoteTime = 0;
    private bool wasGrounded = false;
    private float inpuBuffer = 0;
    private int extraForceTimes = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        CheckGround();
        Jump();
        Fall();
        Move();
       
        wasGrounded = isGrounded;
        jusJumped = false;
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
            extraForceTimes = 0;
        }
        if(inpuBuffer > 0)
        {
            inpuBuffer += Time.deltaTime;
        }
        
    }

    internal void OnMove(Vector2 vector2)
    {
        this.currentSpeed = vector2.x != 0 ? vector2.x * initialSpeed : 0;
        Debug.Log(currentSpeed);
    }
    internal void OnJump(float v)
    {
        jusJumped = v != 0;
        isJumHeld = v != 0;
        if (jusJumped)
            inpuBuffer = 1;
        Debug.Log(jusJumped);

    }
    private void Move()
    {
        if(currentSpeed > 0)
        {
            currentSpeed = currentSpeed + hAcceleration * Time.deltaTime;
        }
        if(currentSpeed < 0)
        {
            currentSpeed = currentSpeed - hAcceleration * Time.deltaTime;

        }
        rb.linearVelocityX = currentSpeed;
    }
    private void Jump()
    {

        if (jusJumped  && !isJumping && (isGrounded || (currentCoyoteTime < coyoteTime && currentCoyoteTime != 0)))
        {
            Debug.Log("SALTO1");
            rb.AddForceY(jumpStrength);
            isJumping = true;
            currentCoyoteTime = 0;
            inpuBuffer = 0;
            
        }
        else if(isJumHeld && !isGrounded && extraForceTimes < 3)
        {
            rb.AddForceY(holdJumpStrength);
            isJumping = true;
            extraForceTimes++;
        }
        else  if(isGrounded && (inpuBuffer < inputBufferTime && inpuBuffer != 0))
        {
            Debug.Log("SALTO2");
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpStrength);
            isJumping = true;
            currentCoyoteTime = 0;
            inpuBuffer = 0;
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
