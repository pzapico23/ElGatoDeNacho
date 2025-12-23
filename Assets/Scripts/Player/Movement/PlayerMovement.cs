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
    private float jumpSpeed = 8;
    [SerializeField]
    private float startingFallSpeed = 2;
    [SerializeField]
    private float maxFallSpeed = 6;
    [SerializeField]
    private float fallAcceleration = 6;
    [SerializeField]
    private float airTime = 0.3f;
    [SerializeField]
    private Vector2 groundCheckBox;
    [SerializeField]
    private float castDistance;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 ceilingCheckBox;
    [SerializeField]
    private float ceilingCastDistance;
    [SerializeField]
    private float coyoteTime = 2;


    private float currentSpeed;
    private bool isJumping = false;
    private bool isJumpHeld = false;
    private bool isGrounded = false;
    private float currentFloor = 0;
    private float currentFallSpeed;
    private bool isInAirTime = false;
    private float currentAirTime = 0;
    private float currentCoyoteTime = 0;
    private bool wasGrounded = false;

    private void Start()
    {
        currentFallSpeed = startingFallSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
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
        if (!isGrounded && !isJumping && !isInAirTime)
        {
            transform.position = transform.position + new Vector3(0, -currentFallSpeed * Time.deltaTime, 0);
            currentFallSpeed = currentFallSpeed + fallAcceleration * Time.deltaTime * Time.deltaTime > maxFallSpeed ? currentFallSpeed + fallAcceleration * Time.deltaTime * Time.deltaTime : maxFallSpeed;
        }
        if (isGrounded)
        {
            currentFallSpeed = startingFallSpeed;
            currentCoyoteTime = 0;
        }
        if(wasGrounded && !isGrounded && !isJumping)
        {
            currentCoyoteTime += Time.deltaTime;
        }
        if (isInAirTime)
        {
            if (IsHittingCeiling())
            {
                isInAirTime = false;
                currentAirTime = 0;
                return;
            }
            currentAirTime += Time.deltaTime;
            if(currentAirTime > airTime)
            {
                isInAirTime = false;
                currentAirTime = 0;
            }
        }
    }

    internal void OnMove(Vector2 vector2)
    {
        this.currentSpeed = vector2.x != 0 ? vector2.x * initialSpeed : 0;
        Debug.Log(currentSpeed);
    }
    internal void OnJump(float v)
    {
        isJumpHeld = v != 0;
    }
    private void Move()
    {
        transform.position = transform.position + new Vector3(currentSpeed * Time.deltaTime, 0, 0);
    }
    private void Jump()
    {
        if(isJumpHeld && (isGrounded || (currentCoyoteTime < coyoteTime && currentCoyoteTime != 0)))
        {
            currentFloor = transform.position.y;
            transform.position = transform.position + new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            isJumping = true;
            currentCoyoteTime = 0;
        }
        if (!isGrounded && isJumping)
        {
            if (IsHittingCeiling())
            {
                isJumping = false;
                return;
            }
            if (transform.position.y - currentFloor < jumpHeight)
            {
                transform.position = transform.position + new Vector3(0, jumpSpeed * Time.deltaTime, 0);
            }
            else
            {
                isJumping = false;
                isInAirTime = true;
            }
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

    private bool IsHittingCeiling()
    {
        return Physics2D.BoxCast(
            transform.position,
            ceilingCheckBox,
            0,
            transform.up,
            ceilingCastDistance,
            groundLayer
        );
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, groundCheckBox);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.up * ceilingCastDistance, ceilingCheckBox);
    }

    
}
