using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FollowPlayer followPlayer;

    void OnDeath()
    {
        if (gameObject.CompareTag("Player"))
        {
            RespawnPlayer(gameManager.CurrentSpawnPoint.position, false);
            SceneManager.LoadScene("Lvl_Nivelin 1");
        } else
        {
            Destroy(gameObject);
        }
    }

    void OnFallen()
    {
        RespawnPlayer(gameManager.LastGroundPosition, true);
    }

    void RespawnPlayer(Vector3 spawnPoint, bool hasFallen)
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
            if (hasFallen)
            {
                followPlayer.SnapToPlayer();
            }
            else
            {
                followPlayer.ResetToInitialPosition();
            }
        }
    }
}
