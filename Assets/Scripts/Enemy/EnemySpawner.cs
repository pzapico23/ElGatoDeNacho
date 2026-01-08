using UnityEngine;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amount;
    [SerializeField] private int delay;

    private List<GameObject> enemyList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnEnemy", delay, delay);
        enemyList = new List<GameObject>();
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
}
