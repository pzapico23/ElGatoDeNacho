using UnityEngine;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amount;
    [SerializeField] private int delay;

    private List<GameObject> enemyList;
    private int hp = 2;
    private int maxHp = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", delay, delay);
        enemyList = new List<GameObject>();
    }

    private void Update()
    {
        if(hp < maxHp)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public void SpawnEnemy()
    {
        enemyList.RemoveAll(e => e == null);
        if(enemyList.Count < amount)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyList.Add(enemyObject);
            EnemyController enemy;
            EnemyBuhoController enemyBuho;
            if (enemy = enemyObject.GetComponent<EnemyController>())
            {
                enemy.Init(player);
            }
            else if ( enemyBuho = enemyObject.GetComponent<EnemyBuhoController>())
            {
                enemyBuho.Init(player);
            }
            else
            {
                EnemyControllerKamikaze enemyKamikaze = enemyObject.GetComponent<EnemyControllerKamikaze>();
                enemyKamikaze.Init(player);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player.GetComponent<Player.PlayerController>().ballModeOn == true)
            {
                hp--;
                if(hp == 0)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (player.GetComponent<Player.PlayerController>().ballModeOn == false)
            {
                player.GetComponent<Player.Health>().Kill();
            }

        }
    }
}
