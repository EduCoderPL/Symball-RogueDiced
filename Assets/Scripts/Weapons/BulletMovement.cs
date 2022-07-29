using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Tooltip("It describes, how long does bullet exist after spawn.")]
    public float lifeTime = 5f;

    [Tooltip("Particle effects that appear when bullet hit something.")]
    public GameObject[] hitEffects;

    [Tooltip("Damage that bullet can give to object that it hit.")]
    public float damage = 10;

    [Tooltip("Force that bullet give after hit.")]
    public float kickbackForce = 1000;

    [Tooltip("If true: bullet will destroy after touching enemies. Else it will move until it touch a wall.")]
    public bool isDestroyedAfterTouch;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Instantiate(hitEffects[0], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Player"))
        {
            HitEventData tempHitEventData = new(collision.gameObject, gameObject, damage, transform.right * kickbackForce);
            RogueDicedEvents.hitEvent.Invoke(tempHitEventData);
            Instantiate(hitEffects[1], transform.position, Quaternion.identity);

            if (isDestroyedAfterTouch)
            {
                Destroy(gameObject);
            }
            
        }

    }
}
