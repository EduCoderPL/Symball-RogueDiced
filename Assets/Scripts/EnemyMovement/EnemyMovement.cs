using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float enemySpeed = 0.1f;
    public float hp = 100;

    private bool isHit;

    private Color[] colorList;
    private int colorIndex;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    public HitPoints hitPoints;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitPoints = GetComponent<HitPoints>();
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }

        isHit = false;
        colorList = new Color[] { Color.white, spriteRenderer.color };
        colorIndex = 0;

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(target!= null)
        {
            Vector2 lookDir = target.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            rb.AddForce(transform.right * Time.deltaTime * enemySpeed);
        }
        

        if (isHit)
        {
            mrugaj();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(Mruganie());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector2 kickBackVector = (collision.transform.position - transform.position).normalized;
            RogueDicedEvents.hitEvent.Invoke(new HitEventData(collision.gameObject, null, 10, kickBackVector));
        }
    }

    void mrugaj()
    {
        colorIndex = (colorIndex + 1) % 2;

        spriteRenderer.color = colorList[colorIndex];
    }

    public void EnemyHit(HitEventData data)
    {
        hitPoints.TakeDamage(data.damage);
        rb.AddForce(- transform.right * data.explosionForce * (1 + rb.velocity.magnitude / 10));      
    }

    private IEnumerator Mruganie()
    {
        isHit = true;
        yield return new WaitForSeconds(2f);
        isHit = false;
        spriteRenderer.color = colorList[1];
    }
}
