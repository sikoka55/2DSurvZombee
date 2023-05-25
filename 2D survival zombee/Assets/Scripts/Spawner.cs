using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform enemyContainer;
    //private EnemyManager _enemyManager;

    [Header("Variables")]
    public float spawnTimer;
    public int inWave;
    public int inWaveMax;

    [Header("EnemiesObjects")]
    public GameObject enemyPrefab;


    [Header("Spawn area")]
    public Transform topLeft;
    public Transform downRight;


    [Header("Not spawn area")]
    public Transform topLeft_not;
    public Transform downRight_not;


    private void Start()
    {
        StartCoroutine(timerForSpawn());
        
    }



    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyContainer);

    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition;

        do
        {
            spawnPosition = new Vector3(
                Random.Range(topLeft.position.x, downRight.position.x),
                Random.Range(topLeft.position.y, downRight.position.y),
                0f
                );
        } while (IsInNoSpawnArea(spawnPosition));
        return spawnPosition;

    }

    private bool IsInNoSpawnArea(Vector3 position)
    {
        return position.x >= topLeft_not.position.x && position.x <= downRight_not.position.x &&
              position.y >= downRight_not.position.y && position.y <= topLeft_not.position.y;
    }

    public IEnumerator timerForSpawn()
    {
        while (inWave < inWaveMax)
        {

            yield return new WaitForSeconds(spawnTimer);
            SpawnEnemy();
            inWave++;

        }
    }
}
