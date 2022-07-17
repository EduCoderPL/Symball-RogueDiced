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
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Vector2 lookDir = target.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.AddForce(transform.right * Time.deltaTime * enemySpeed);


        if (hp <= 0) 
        {
            Destroy(gameObject);
            Interface.points += 100;
        }

        if (isHit)
        {
            mrugaj();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        isHit = true;
    }

    void mrugaj()
    {
        colorIndex = (colorIndex + 1) % 2;

        spriteRenderer.color = colorList[colorIndex];
    }
}
