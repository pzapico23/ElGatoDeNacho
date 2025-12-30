using System;
using Player;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FollowPlayer followPlayer;

    void OnDeath()
    {
        if (gameObject.CompareTag("Player"))
        {
            RespawnPlayer(gameManager.CurrentSpawnPoint.position);
        } else
        {
            Destroy(gameObject);
        }
    }

    void OnFallen()
    {
        RespawnPlayer(gameManager.LastGroundPosition);
    }

    void RespawnPlayer(Vector3 spawnPoint)
    {
        transform.position = spawnPoint;

        GetComponent<PlayerController>().ForceExitBallMode();

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
