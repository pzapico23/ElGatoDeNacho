using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private bool ballModeOn = false;

        [SerializeField] private int currentBallTimer;
        [SerializeField] private int maxBallTimer;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite ballSprite;
        [SerializeField] private FollowPlayer cameraFollow;


        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        internal void OnModeChange()
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
            playerMovement.enabled = true;
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
            cameraFollow.SetIsFollowing(true);

        }

        private void StartBallMode()
        {
            currentBallTimer = 0;
            playerMovement.enabled = false;
            InvokeRepeating("IncreaseTimer", 1f, 1f);
            GetComponent<SpriteRenderer>().sprite = ballSprite;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            cameraFollow.SetIsFollowing(false);

        }

        private void IncreaseTimer()
        {
            currentBallTimer++;
            if(currentBallTimer == maxBallTimer)
            {
                StopBallMode();
            }
        }
    }
}
