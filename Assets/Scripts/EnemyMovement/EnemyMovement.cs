using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitPoints))]
public class EnemyMovement : MonoBehaviour, IEnemy
{
    public Transform target;
    public float enemySpeed = 0.1f;
    public HitPoints hitPoints;


    private bool isShocked;
    private Color[] colorList;
    private int colorIndex;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitPoints = GetComponent<HitPoints>();
        rb = GetComponent<Rigidbody2D>();


        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }

        isShocked = false;
        colorList = new Color[] { Color.white, spriteRenderer.color };
        colorIndex = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if(target!= null)
        {
            Rotate();
            Move();
        }
        
        Blink();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(BlinkingCooldown());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector2 kickBackVector = transform.right;
            RogueDicedEvents.hitEvent.Invoke(new HitEventData(collision.gameObject, null, 10, kickBackVector));
        }
    }

    private void Rotate()
    {
        Vector2 lookDir = target.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Move()
    {
        rb.AddForce(enemySpeed * Time.deltaTime * transform.right);
    }

    void Blink()
    {
        if (isShocked)
        {
            colorIndex = (colorIndex + 1) % 2;
            spriteRenderer.color = colorList[colorIndex];
        }

    }

    public void EnemyHit(HitEventData data)
    {
        hitPoints.TakeDamage(data.damage);
        rb.AddForce(data.explosionForce);      
    }

    private IEnumerator BlinkingCooldown()
    {
        isShocked = true;
        yield return new WaitForSeconds(2f);
        isShocked = false;
        spriteRenderer.color = colorList[1];
    }
}
