using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberEnemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    public GameObject explosionParticle;

    public float radius = 100f;
    public float explosionForceCoef = 1000f;
    public float explodeDistance = 10f;

    Collider2D[] colliders = null;

    public LayerMask layerToHit = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        Physics2D.autoSyncTransforms = true;
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - enemyMovement.target.position).magnitude < explodeDistance)
        {
            StartCoroutine(Explode());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(Explode());

        }
    }


    IEnumerator Explode()
    {

        Destroy(enemyMovement);
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        yield return new WaitForSeconds(1f);
        Knockback();
        Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Knockback()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D o in colliders)
        {
            Rigidbody2D rb = o.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 distex = o.transform.position - transform.position;
                if (distex.magnitude > 0)
                {
                    float explosionForce = explosionForceCoef / distex.magnitude;
                    rb.AddForce(distex.normalized * explosionForce);

                    if (o.transform.CompareTag("Player"))
                    {
                        PlayerMovement.hp -= (explosionForce / 80);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
