using System;
using Unity.VisualScripting;
using UnityEngine;
using Player;

public class EnemyBuhoController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private bool seePlayer = false;
    private Vector3 _originalPosition;
    private Vector3 _upRange;
    private Vector3 _downRange;
    private bool isRight = true;
    private bool needReturn = false;
    private Health health;
    private Vector3 lastPlayerPosition;
    private bool attacking = false;

    [SerializeField] private float patrolRange;
    [SerializeField] private Vector2 playerCheckBox;
    [SerializeField] private Vector3 castCheckPlayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float barValue = 15f;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        _rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        _rigidBody.linearVelocityX = velocity;
        _originalPosition = transform.position;
        _upRange = new Vector3(transform.position.x + patrolRange, transform.position.y , transform.position.z);
        _downRange = new Vector3(transform.position.x - patrolRange, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.identity;
        animator = GetComponent<Animator>();
        _rigidBody.freezeRotation = true;

    }
    public void Init(GameObject player)
    {
        this.player = player;
        this.playerController = player.GetComponent<PlayerController>();
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

        if (seePlayer == false && playerController.ballModeOn == false && !needReturn && !attacking)
        {

            _rigidBody.linearVelocityX = _rigidBody.linearVelocityX > 0 ? velocity : -velocity;
            _rigidBody.linearVelocityY = 0;
            
            if (transform.position.x > _upRange.x || transform.position.x < _downRange.x)
            {
                _rigidBody.linearVelocityX *= -1;
            }
        }
    }

    private void Return()
    {
        if(seePlayer == false && playerController.ballModeOn == false && needReturn)
        {
            if ((transform.position.x < _originalPosition.x && !isRight) || (transform.position.x > _originalPosition.x && isRight))
            {
                isRight = !isRight;
                transform.Rotate(0f, 180f, 0f);
                castCheckPlayer.x *= -1;
            }

            _rigidBody.linearVelocity = (_originalPosition - transform.position).normalized * acceleration;

            if ( Math.Abs(transform.position.y - _originalPosition.y) < 0.1)
            {
                needReturn = false;
            }
        }
    }

    private void Attack()
    {
        if (seePlayer == true && playerController.ballModeOn == false && !attacking)
        {
            _rigidBody.linearVelocity = (player.transform.position - transform.position).normalized * acceleration;
            animator.SetBool("isAttacking", true);
            attacking = true;
            lastPlayerPosition = player.transform.position;
        }
        else if(!attacking)
        {
            animator.SetBool("isAttacking", false);
        }

        if (Math.Abs(transform.position.x - lastPlayerPosition.x) < 0.1 && Math.Abs(transform.position.y - lastPlayerPosition.y) < 0.1)
        {
            attacking = false;
            needReturn = true;
        }
    }

    private void Escape()
    {
        if (playerController.ballModeOn == true)
        {
            if (transform.position.x < player.transform.position.x && transform.position.x > _downRange.x)
            {
                _rigidBody.linearVelocityX = -acceleration;
                needReturn = true;
            }
            else if (transform.position.x > player.transform.position.x && transform.position.x < _upRange.x)
            {
                _rigidBody.linearVelocityX = acceleration;
                needReturn = true;
            }
            else
            {
                _rigidBody.linearVelocityX = 0;
                needReturn = true;
            }
        }
    }

    private void Facing()
    {
        if (((_rigidBody.linearVelocityX > 0 && !isRight) || (_rigidBody.linearVelocityX < 0 && isRight)) && !needReturn)
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
                Debug.Log("Enemy defeated! Bar value gained: " + barValue);
                playerController.AddToBallMeter(barValue);
                health.Kill();
            }
            else if (playerController.ballModeOn == false)
            {
                player.GetComponent<Health>().Kill();
                attacking = false;
                needReturn = true;
            }

        }
    }

    private void CheckPlayer()
    {
        if (Physics2D.BoxCast(transform.position + castCheckPlayer, playerCheckBox, 0, transform.forward, 0, playerLayer))
        {
            if(attacking || needReturn)
            {
                seePlayer = false;
            }
            else
            {
                seePlayer = true;
            }
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
