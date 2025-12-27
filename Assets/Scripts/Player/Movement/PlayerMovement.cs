using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 4;
    [SerializeField]
    private float initialSpeed = 2;
    [SerializeField]
    private float hAcceleration = 1;
    [SerializeField]
    private float jumpStrength = 5;
    [SerializeField]
    private float holdJumpStrength = 3;
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
    [SerializeField]
    private float inputBufferTime = 4;


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
        Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * hAcceleration);
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
            extraForceTimes++;
        }
        else  if(isGrounded && (inpuBuffer < inputBufferTime && inpuBuffer != 0))
        {
            Debug.Log("SALTO2");
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpStrength * 2);
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
