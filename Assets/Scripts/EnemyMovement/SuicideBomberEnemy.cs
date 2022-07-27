using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberEnemy : MonoBehaviour, IDeathEffect
{
    private EnemyMovement enemyMovement;
    public GameObject explosionParticle;

    public float radius = 100f;
    public float explosionForceCoef = 1000f;
    public float explodeDistance = 10f;
    public float timeToDetonate = 1f;

    Collider2D[] colliders = null;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        Physics2D.autoSyncTransforms = true;
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        if (enemyMovement.target != null)
        {
            if((transform.position - enemyMovement.target.position).magnitude < explodeDistance)
            {
                StartCoroutine(Explode());
            }
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
        yield return new WaitForSeconds(timeToDetonate);
        Knockback();
        Instantiate(explosionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Knockback()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D o in colliders)
        {
            if (o.TryGetComponent<Rigidbody2D>(out var rb))
            {
                Vector2 distex = o.transform.position - transform.position;
                if (distex.magnitude > 0)
                {
                    float explosionForce = explosionForceCoef / distex.magnitude;
                    rb.AddForce(distex.normalized * explosionForce);

                    if (o.transform.CompareTag("Player"))
                    {
                        o.transform.GetComponent<HitPoints>().TakeDamage(explosionForce / 80);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Die()
    {
        StartCoroutine(Explode());
    }
}
