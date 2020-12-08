using System.Collections.Generic;
using UnityEngine;

public class EnemiesManagerScript : MonoBehaviour 
{
    public int poolSize;
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    Vector3 spawnPosition = new Vector3(0f, 0f, 2.65f);
    float spawnNextEnemyTimeMin = 0.5f;
    float spawnNextEnemyTimeMax = 2f;
    float spawnNextEnemyTime;
    float spawnNextEnemyTimer;
    float coordMinX = -4.3f;
    float coordMaxX = 4.3f;
    List<GameObject> enemiesPool;
    private bool isPaused;

    void Awake()
    {
        this.enemiesPool = new List<GameObject>();

        for (int i = 0; i < this.poolSize; i++)
        {
            this.enemiesPool.Add(CreateEnemy());
        }
    }

    void Start()
    {
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        GameObject.FindWithTag("Player").GetComponent<PlayerLogicScript>().pauseGame += PauseGame;
    }

	void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (spawnNextEnemyTimer < spawnNextEnemyTime)
        {
            spawnNextEnemyTimer += Time.deltaTime;
        }
        else
        {
            EnableEnemy();
        }
	}

    GameObject CreateEnemy()
    {
        GameObject enemyShip = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
        enemyShip.GetComponent<EnemyScript>().bulletPrefab = enemyBulletPrefab;
        enemyShip.transform.parent = this.transform;

        return enemyShip;
    }

    void EnableEnemy()
    {
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        var enemy = default(GameObject);

        for (int index = 0; index < poolSize; index++)
        {
            var enemyAtIndex = enemiesPool[index];
            if (!enemyAtIndex.activeSelf)
            {
                enemy = enemyAtIndex;
                break;
            }
        }

        var x = Random.Range(coordMinX, coordMaxX);
        enemy.transform.position = new Vector3(x, enemy.transform.position.y, enemy.transform.position.z);
        enemy.SetActive(true);
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
    }
}