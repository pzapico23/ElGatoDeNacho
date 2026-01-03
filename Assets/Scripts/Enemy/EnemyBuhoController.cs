using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBuhoController : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private Rigidbody2D _rigidBody;
    private bool seePlayer = false;
    private Vector2 _originalPosition;
    private float _upRange;
    private float _downRange;

    [SerializeField] private float patrolRange;
    [SerializeField] private Vector2 playerCheckBox;
    [SerializeField] private Vector3 castCheckPlayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.linearVelocityX = velocity;
        _originalPosition = transform.position;
        _upRange = transform.position.x + patrolRange;
        _downRange = transform.position.x - patrolRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPlayer();
        Patrol();
        Attack();
    }

    private void Patrol()
    { 
        if (seePlayer == false)
        {
            if (transform.position.x > _upRange)
            {
                transform.Rotate(0f, 180f, 0f);
                castCheckPlayer.x *= -1;
                _rigidBody.linearVelocityX = velocity * -1;
            }
            else if (transform.position.x < _downRange)
            {
                transform.Rotate(0f, 180f, 0f);
                castCheckPlayer.x *= -1;
                _rigidBody.linearVelocityX = velocity * -1;
            }
        }

    }

    private void Attack()
    {
        if (seePlayer == true)
        {
            _rigidBody.linearVelocityX = acceleration;
            _rigidBody.linearVelocityY = -acceleration;
        }
//        transform.position = _originalPosition;
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
