using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timeToSpawn = 10f;
    // Start is called before the first frame update
    public GameObject[] listOfEnemies;
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
            GameObject test = Instantiate(newEnemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(Random.Range(timeToSpawn/2, timeToSpawn));
        }


    }
}
