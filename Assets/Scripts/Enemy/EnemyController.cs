using Player;
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
    [SerializeField] private float acceleration;
    [SerializeField] private float velocity;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;

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
        Escape();
    }

    private void Patrol()
    {
        if(seePlayer == false && isGrounded == false && playerController.ballModeOn == false)
        {
            transform.Rotate(0f, 180f, 0f);
            castDistance.x *= -1;
            castCheckPlayer.x *= -1;
            _rigidBody.linearVelocityX *= -1;
        }
    }

    private void Attack()
    {
        if (seePlayer == true && isGrounded == true && playerController.ballModeOn == false)
        {
            _rigidBody.linearVelocityX = acceleration;
        }
    }

    private void Escape()
    {
        if (isGrounded == true && playerController.ballModeOn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -1 * acceleration * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (playerController.ballModeOn == true && collider.gameObject.CompareTag("Player"))
        {
            Destroy(collider.gameObject);
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
