using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float lifeTime = 5f;
    public GameObject hitEffect;

    public bool destroyAfterEnemyTouch;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            EnemyMovement temp = collision.gameObject.GetComponent<EnemyMovement>();
            temp.hp -= 10;
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);

            if (destroyAfterEnemyTouch)
            {
                Destroy(gameObject);
            }
            
        }

    }
}
