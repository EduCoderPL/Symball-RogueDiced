using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponGenerator : MonoBehaviour
{
    public static int[] numbers;
    public float timeToRandomize = 60f;
    void Start()
    {
        numbers = new int[]{1, 3};
        StartCoroutine(RandomNumbersGo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RandomNumbersGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToRandomize);
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 6);

            }
        }

    }
}
