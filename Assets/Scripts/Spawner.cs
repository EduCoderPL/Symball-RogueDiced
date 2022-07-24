using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timeToSpawn = 10f;
    // Start is called before the first frame update
    public Transform[] listOfSpawners;
    public GameObject[] listOfEnemies;

    public Transform enemyEmptyObject;
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            GameObject newEnemy = listOfEnemies[Random.Range(0, listOfEnemies.Length)];
            Transform activeSpawner = listOfSpawners[Random.Range(0, listOfSpawners.Length)];

            GameObject test = Instantiate(newEnemy, activeSpawner.position, transform.rotation);
            test.transform.SetParent(enemyEmptyObject);
            yield return new WaitForSeconds(Random.Range(timeToSpawn/2, timeToSpawn));
        }
    }
}
