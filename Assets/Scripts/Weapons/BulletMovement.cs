using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float lifeTime = 5f;
    public GameObject hitEffect;

    public float damage = 10;
    public float explosionForce = 1000;
    public bool destroyAfterEnemyTouch;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            RogueDicedEvents.hitEvent.Invoke(new HitEventData(collision.gameObject, gameObject, damage, transform.right * explosionForce));
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            if (destroyAfterEnemyTouch)
            {
                Destroy(gameObject);
            }
            
        }

    }
}
