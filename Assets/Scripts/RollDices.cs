using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDices : MonoBehaviour
{
    [Tooltip("Randomize weapon cooldown.")]
    public float timeToRandomize = 30f;

    [SerializeField] int[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomNumbersGo());
        RogueDicedEvents.rollDiceEvent.Invoke(new RollDiceEventData(numbers));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomNumbersGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToRandomize - 3);
            RogueDicedEvents.rollInfoEvent.Invoke();
            yield return new WaitForSeconds(3);


            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                
            }
            RogueDicedEvents.rollDiceEvent.Invoke(new RollDiceEventData(numbers));
        }

    }
}
