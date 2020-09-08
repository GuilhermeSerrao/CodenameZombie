using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] smallEnemies, bigEnemies;

    [SerializeField]
    private int enemiesToSpawn;

    [SerializeField]
    private float timer, reduceTime, spawnX, spawnY;

    private float startTimer;

    private int location, spawnedEnemies;

    private Spawners spawnControl;
    // Start is called before the first frame update
    void Start()
    {
        spawnControl = FindObjectOfType<Spawners>();
        startTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {            
            
            timer = startTimer;
            if (startTimer > 0.5)
            {
                startTimer -= reduceTime;
            }
            
            
            RandomSide();
            Vector3 spawnLocation = new Vector3();
            switch (location)
            {
                case 1:
                    spawnLocation = new Vector3(Random.Range(-spawnX, spawnX), spawnY, 0);
                 
                    SpawnEnemy(spawnLocation);
                    break;
                case 2:
                    spawnLocation = new Vector3(spawnY, Random.Range(-spawnX, spawnX), 0);
                    SpawnEnemy(spawnLocation);
                    break;
                case 3:
                    spawnLocation = new Vector3(Random.Range(-spawnX, spawnX), -spawnY, 0);
                    SpawnEnemy(spawnLocation);
                    break;
                case 4:
                    spawnLocation = new Vector3(-spawnY, Random.Range(-spawnX, spawnX), 0);
                    SpawnEnemy(spawnLocation);
                    break;
                default:
                    break;
            }
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        spawnControl.AddZombie();
        if (Random.Range(0, 6) > 4)
        {
            Instantiate(bigEnemies[Random.Range(0, bigEnemies.Length - 1)], position, Quaternion.identity);
        }
        else
        {
            Instantiate(smallEnemies[Random.Range(0, bigEnemies.Length - 1)], position, Quaternion.identity);
        }

    }

    private void RandomSide()
    {
        location = Random.Range(1,5);      
        
    }
}
