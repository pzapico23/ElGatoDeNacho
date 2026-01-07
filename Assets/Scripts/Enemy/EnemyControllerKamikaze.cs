using NUnit.Framework.Constraints;
using Player;
using UnityEngine;

public class EnemyControllerKamikaze : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private bool isGrounded = true;
    private bool seePlayer = false;
    private bool isRight = true;
    private Health health;

    [SerializeField] private Vector2 groundCheckBox;
    [SerializeField] private Vector3 castDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 playerCheckBox;
    [SerializeField] private Vector3 castCheckPlayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float acceleration;
    [SerializeField] private float velocity;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        _rigidBody.linearVelocityX = velocity;
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
        CheckPlayer();
        Patrol();
        Attack();
        Escape();
        Facing();
    }

    private void Patrol()
    {
        if (seePlayer == false && playerController.ballModeOn == false)
        {
            _rigidBody.linearVelocityX = _rigidBody.linearVelocityX > 0 ? velocity : -velocity;

            if (isGrounded == false)
            {
                _rigidBody.linearVelocityX *= -1;
            }
        }
    }

    private void Attack()
    {
        if (seePlayer == true && playerController.ballModeOn == false)
        {
            _rigidBody.linearVelocityX = _rigidBody.linearVelocityX > 0 ? acceleration : -acceleration;
        }
    }

    private void Escape()
    {
        if (isGrounded == true && playerController.ballModeOn == true && transform.position.x < player.transform.position.x)
        {
            _rigidBody.linearVelocityX = -acceleration;
        } else if (isGrounded == true && playerController.ballModeOn == true && transform.position.x > player.transform.position.x) {
            _rigidBody.linearVelocityX = acceleration;
        } else if (isGrounded == false && playerController.ballModeOn == true)
        {
            _rigidBody.linearVelocityX = 0;
        }
    }

    private void Facing()
    {
        if ((_rigidBody.linearVelocityX > 0 && !isRight) ||(_rigidBody.linearVelocityX < 0 && isRight))
        {
            isRight = !isRight;
            transform.Rotate(0f, 180f, 0f);
            castDistance.x *= -1;
            castCheckPlayer.x *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.ballModeOn == true)
            {
                health.Kill();
            } else if (playerController.ballModeOn == false)
            {
                player.GetComponent<Health>().Kill();
            }
            
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
