using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody2D _rigidBody;
    private bool isGrounded = true;
    private bool seePlayer = false;
    [SerializeField] private Vector2 groundCheckBox;
    [SerializeField] private Vector3 castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 playerCheckBox;
    [SerializeField] private Vector3 castCheckPlayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.linearVelocityX = velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
        CheckPlayer();
        Patrol();
        Attack();
    }

    private void Patrol()
    {
        if(isGrounded == false)
        {
            transform.Rotate(0f, 180f, 0f);
            castDistance.x *= -1;
            castCheckPlayer.x *= -1;
            _rigidBody.linearVelocityX *= -1;
        }
    }

    private void Attack()
    {
        if (seePlayer == true && isGrounded == true)
        {
            _rigidBody.linearVelocityX = 5;
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

    private void CheckPlayer()
    {
        if (Physics2D.BoxCast(transform.position + castCheckPlayer, playerCheckBox, 0, transform.forward, 0, playerLayer))
        {
            seePlayer = true;
        }
        else
        {
            seePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + castDistance, groundCheckBox);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + castCheckPlayer, playerCheckBox);
    }
}
