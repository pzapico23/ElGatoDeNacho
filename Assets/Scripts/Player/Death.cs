using Player;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDeath()
    {
        RespawnPlayer(gameManager.CurrentSpawnPoint);
    }

    void RespawnPlayer(Transform spawnPoint)
    {
        this.transform.position = spawnPoint.position;
        this.GetComponent<Health>().ResetHealth(); 
    }
}
