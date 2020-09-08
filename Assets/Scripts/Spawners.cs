using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField]
    private int zombiesPerSpawn;

    [SerializeField]
    private GameObject spawner;

    private int numSpawners = 1, zombiesSpawned;

    private void Update()
    {

        if (zombiesSpawned / numSpawners >= zombiesPerSpawn)
        {
            zombiesSpawned = 0;
            var newSpawn = Instantiate(spawner, transform.position, transform.rotation);
            newSpawn.transform.parent = transform;
            numSpawners++;
        }

    }

    public void AddZombie()
    {
        zombiesSpawned++;
    }

}
