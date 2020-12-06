using UnityEngine;
using System.Collections;

public class EnemiesManagerScript : MonoBehaviour 
{
    public GameObject enemyPrefab;
    public GameObject enemyBulletPrefab;
    Vector3 spawnPosition = new Vector3(0f, 0f, 2.65f);
    float spawnNextEnemyTimeMin = 0.5f;
    float spawnNextEnemyTimeMax = 2f;
    float spawnNextEnemyTime;
    float spawnNextEnemyTimer;
    float coordMinX = -4.3f;
    float coordMaxX = 4.3f;
    private bool isPaused;

    void Start()
    {
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        GameObject.FindWithTag("Player").GetComponent<PlayerLogicScript>().pauseGame += PauseGame;
    }

	void Update ()
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
            SpawnEnemy();
        }
	}

    void SpawnEnemy()
    {
        spawnPosition.x = Random.Range(coordMinX, coordMaxX);
        spawnNextEnemyTime = Random.Range(spawnNextEnemyTimeMin, spawnNextEnemyTimeMax);
        spawnNextEnemyTimer = 0f;

        GameObject enemyShip = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0f, 180f, 0f)) as GameObject;
        enemyShip.GetComponent<EnemyScript>().bulletPrefab = enemyBulletPrefab;
        enemyShip.transform.parent = this.transform;
    }

    private void PauseGame()
    {
        isPaused = !isPaused;
    }

}
