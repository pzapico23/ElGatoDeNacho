using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created


        [SerializeField] private float maxBallMeter = 100;
        [SerializeField] private float startingBallMeter = 30;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite ballSprite;
        [SerializeField] private FollowPlayer cameraFollow;
        [SerializeField] private float shootForce = 1000;
        [SerializeField] private float forceHoldGrow = 3;
        [SerializeField] private float maxAimDistance = 10;
        [SerializeField] private LineRenderer line;


        private bool ballModeOn = false;
        private float currentBallMeter;
        private Vector3 mousePosition;
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider2D;
        private CircleCollider2D circleCollider2D;
        private Rigidbody2D rigidbody2D;
        private bool changingMode = false;
        private Vector3 currentEnd;
        private float currentHoldDistance = 0;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            circleCollider2D = GetComponent<CircleCollider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            currentBallMeter = startingBallMeter;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePosition.z = 0;

            if (changingMode)
            {
                line.enabled = true;
                Vector3 end = transform.position + (mousePosition - transform.position).normalized * maxAimDistance;

                currentHoldDistance = Mathf.Lerp(currentHoldDistance, maxAimDistance, Time.deltaTime * forceHoldGrow);

                currentEnd = transform.position + (mousePosition - transform.position).normalized * currentHoldDistance;

                line.SetPosition(0, transform.position);
                line.SetPosition(1, currentEnd);
            }
            else
            {
                line.enabled = false;
                currentEnd = transform.position;
                currentHoldDistance = 0;
            }
        }

        internal void OnModeChangeStart()
        {
            
            if (!ballModeOn)
            {
                spriteRenderer.sprite = ballSprite;
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = true;
                playerMovement.enabled = false;
                changingMode = true;
                rigidbody2D.gravityScale = 0;
                rigidbody2D.linearVelocity = Vector2.zero;
            }

        }

        internal void OnModeChangeFinish()
        {
            ballModeOn = !ballModeOn;
            if (ballModeOn)
            {
                StartBallMode();
            }
            else
            {
                StopBallMode();
            }

        }

        private void StopBallMode()
        {
            CancelInvoke("IncreaseTimer");
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            playerMovement.enabled = true;
            spriteRenderer.sprite = defaultSprite;
            boxCollider2D.enabled = true;
            circleCollider2D.enabled = false;
            rigidbody2D.freezeRotation = true;
            cameraFollow.SetIsFollowing(true);
            rigidbody2D.gravityScale = 1;
            transform.rotation = Quaternion.identity;
        }

        private void StartBallMode()
        {
            InvokeRepeating("IncreaseTimer", 1f, 1f);
            rigidbody2D.freezeRotation = false;
            cameraFollow.SetIsFollowing(false);
            rigidbody2D.gravityScale = 1;

            Vector2 direction = (new Vector2(currentEnd.x, currentEnd.y) - rigidbody2D.position).normalized;
            rigidbody2D.AddForce(direction * shootForce * (currentHoldDistance / maxAimDistance));
            changingMode = false;


        }

        private void IncreaseTimer()
        {
            currentBallMeter--;
            if(currentBallMeter == 0)
            {
                StopBallMode();
            }
        }

        public float getCurrentBallMeter()
        {
            return currentBallMeter;
        }
        public float getMaxBallMeter()
        {
            return maxBallMeter;
        }
    }
}
