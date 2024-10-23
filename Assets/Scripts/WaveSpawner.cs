using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]//Serializable so it shows up in editor
    public class Wave//Wave class 
    {
        public Enemy[] enemies;//different type of enemies to use
        public int count;//no of enemies
        public float timeBetweenSpawns;//time between spawns of enemies of same wave
    }

    public Wave[] waves;//instance of Wave class that we manipulate in editor
    public Transform[] spawnpoints;//Random spawn points(multiple) where enemies spawn
    public float timeBetweenWaves;//Time between different waves spawning

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;//check if all enemies of wave are spawned

    public GameObject boss;//for boss 
    public Transform bossSpawnPoint;

    public GameObject healthBar;//for UI health element

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//player transform component
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);//waiting time between waves
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];//assigning the waves to be spawned to a single attainable Wave class instance(like temp)

        for (int i = 0; i < currentWave.count; i++)//loop until count 
        {
            if (player == null)//safe check if player dies no need to spawn enemies
            {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];//random enemies selected to spawn
            Transform randomSpot = spawnpoints[Random.Range(0, spawnpoints.Length)];//random spawn point
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);//instantiate random enemy at random spot 

            if (i == currentWave.count - 1)//if count is at last instance wave complete finishedspawning bool variable set to true else false
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);//waiting time between spawns
        }
    }

    private void Update()
    {
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)//finished spawning and no enemies left in scene
        {
            finishedSpawning = false;//reset variable
            if (currentWaveIndex + 1 < waves.Length)//if waves are still left to be spawned 
            {
                currentWaveIndex++;//increase current index so wave[2] say is spawned after wave[1]
                StartCoroutine(StartNextWave(currentWaveIndex));//call coroutine to spawn next wave with updated index
            }
            else
            {
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                healthBar.SetActive(true);
            }
        }
    }
}
