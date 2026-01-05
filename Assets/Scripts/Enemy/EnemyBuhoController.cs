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
    private bool isRight = true;
    private bool goodLocation = true;
    private Health health;

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
        health = GetComponent<Health>();
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
        Return();
        Attack();
        Escape();
        Facing();
    }

    private void Patrol()
    {
        if (seePlayer == false && playerController.ballModeOn == false && goodLocation == true)
        {
            _rigidBody.linearVelocityX = _rigidBody.linearVelocityX > 0 ? velocity : -velocity;
            
            if (transform.position.x > _upRange.x || transform.position.x < _downRange.x)
            {
                _rigidBody.linearVelocityX *= -1;
            }
        }

    }

    private void Return()
    {
        if(seePlayer == false && playerController.ballModeOn == false && goodLocation == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _originalPosition, velocity * Time.deltaTime);
            goodLocation = true;
        }
    }

    private void Attack()
    {
        if (seePlayer == true && playerController.ballModeOn == false)
       { 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, acceleration * Time.deltaTime);
            goodLocation = false;
        }
    }

    private void Escape()
    {
        if (playerController.ballModeOn == true && transform.position.x < player.transform.position.x)
        {
            _rigidBody.linearVelocityX = -acceleration;
            goodLocation = false;
        } else if (playerController.ballModeOn == true && transform.position.x > player.transform.position.x)
        {
            _rigidBody.linearVelocityX = acceleration;
            goodLocation = false;
        } else if(playerController.ballModeOn == true)
        {
            _rigidBody.linearVelocityX = 0;
        }
    }

    private void Facing()
    {
        if ((_rigidBody.linearVelocityX > 0 && !isRight) || (_rigidBody.linearVelocityX < 0 && isRight))
        {
            isRight = !isRight;
            transform.Rotate(0f, 180f, 0f);
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
            }
            else if (playerController.ballModeOn == false)
            {
                player.GetComponent<Health>().Kill();
            }

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
