using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        RogueDicedEvents.hitEvent.AddListener(MakeDamage);
    }

    void MakeDamage(HitEventData data)
    {
        if (data.victim.CompareTag("Enemy"))
        {
            data.victim.GetComponent<EnemyMovement>().EnemyHit(data);
        }
        if (data.victim.CompareTag("Player"))
        {
            data.victim.GetComponent<PlayerMovement>().PlayerHit(data);
        }

    }

}
