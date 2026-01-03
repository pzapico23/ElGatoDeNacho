using System;
using Unity.VisualScripting;
using UnityEngine;
using Player;

public class EnemyBuhoController : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody2D _rigidBody;
    private bool seePlayer = false;
    private Vector3 _originalPosition;
    private Vector3 _upRange;
    private Vector3 _downRange;

    [SerializeField] private float patrolRange;
    [SerializeField] private Vector2 playerCheckBox;
    [SerializeField] private Vector3 castCheckPlayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.linearVelocityX = velocity;
        _originalPosition = transform.position;
        _upRange = new Vector3(transform.position.x + patrolRange, transform.position.y , transform.position.z);
        _downRange = new Vector3(transform.position.x - patrolRange, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPlayer();
        Patrol();
        Attack();
        Escape();
    }

    private void Patrol()
    { 
        if (seePlayer == false && playerController.ballModeOn == false)
        {
            if (transform.position.x > _upRange.x || transform.position.x < _downRange.x)
            {
                transform.Rotate(0f, 180f, 0f);
                castCheckPlayer.x *= -1;
                _rigidBody.linearVelocityX *= -1;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _originalPosition, velocity * Time.deltaTime);
        }

    }

    private void Attack()
    {
        if (seePlayer == true && playerController.ballModeOn == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, acceleration * Time.deltaTime);
        }
    }

    private void Escape()
    {
        if (playerController.ballModeOn == true)
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + castCheckPlayer, playerCheckBox);
    }
}
