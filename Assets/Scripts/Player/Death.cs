using Player;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FollowPlayer followPlayer;

    void OnDeath()
    {
        RespawnPlayer(gameManager.CurrentSpawnPoint);
    }

    void RespawnPlayer(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;

        GetComponent<PlayerController>().ForceExitBallMode();
        
        GetComponent<Health>().ResetHealth(); 

        StopOutsideCamera stopOutsideCamera = GetComponent<StopOutsideCamera>();
        if (stopOutsideCamera)
        {
            stopOutsideCamera.NotifyRespawned();
        }

        if (followPlayer)
        {
            followPlayer.SnapToPlayer();
        }
    }
}
