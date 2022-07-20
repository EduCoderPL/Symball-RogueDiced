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

            rb.AddForce(enemySpeed * Time.deltaTime * transform.right);
        }
        

        if (isHit)
        {
            Blink();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(BlinkingControl());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector2 kickBackVector = transform.right;
            RogueDicedEvents.hitEvent.Invoke(new HitEventData(collision.gameObject, null, 10, kickBackVector));
        }
    }

    void Blink()
    {
        colorIndex = (colorIndex + 1) % 2;

        spriteRenderer.color = colorList[colorIndex];
    }

    public void EnemyHit(HitEventData data)
    {
        hitPoints.TakeDamage(data.damage);
        rb.velocity = Vector2.zero;
        rb.AddForce(data.explosionForce);      
    }

    private IEnumerator BlinkingControl()
    {
        isHit = true;
        yield return new WaitForSeconds(2f);
        isHit = false;
        spriteRenderer.color = colorList[1];
    }
}
