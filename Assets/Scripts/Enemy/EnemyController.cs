using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody2D _rigidBody;
    private Vector3 m_yVelocity = Vector3.zero;
    private bool isGrounded = false;
    private bool wasGrounded = false;
    [SerializeField] private Vector2 groundCheckBox;
    [SerializeField] private Vector3 castDistance;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        CheckGround();
    }

    private void Patrol()
    {
        _rigidBody.linearVelocityX = velocity;

        if(isGrounded == false)
        {
            _rigidBody.linearVelocityX = -velocity;
        }
        else
        {
            _rigidBody.linearVelocityX = velocity;
        }
    }

    private void CheckGround()
    {
        if (Physics2D.BoxCast(transform.position + castDistance, groundCheckBox, 0, -transform.up, 0, groundLayer))
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
        Gizmos.DrawWireCube(transform.position + castDistance, groundCheckBox);
    }
}
