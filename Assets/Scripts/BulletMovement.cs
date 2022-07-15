using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float lifeTime = 5f;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            if (temp.hp <= 0)
            {
                Destroy(collision.gameObject);
            }
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
