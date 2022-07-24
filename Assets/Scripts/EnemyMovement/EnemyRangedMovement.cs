using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedMovement : MonoBehaviour
{

    public Transform target;
    public float enemySpeed = 0.1f;

    public float fireRange = 3f;

    private Rigidbody2D rb;
    public HitPoints hitPoints;

    private bool canFireToTarget;

    private FireBulletsEnemy weapon;

    private bool isKnockedOut;

    private bool isHit;

    private Color[] colorList;
    private int colorIndex;


    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        canFireToTarget = false;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<FireBulletsEnemy>();
        isKnockedOut = false;
        isHit = false;

        colorList = new Color[] { Color.black, spriteRenderer.color };
        colorIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !isKnockedOut)
        {
            CheckIfTargetInRange();
            if (!canFireToTarget)
            {
                Vector2 lookDir = target.position - transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                rb.AddForce(enemySpeed * Time.deltaTime * transform.right);
            }

            else
            {
                rb.AddForce(-enemySpeed * Time.deltaTime * transform.right);
                Vector2 lookDir = target.position - transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            
        }
        if (isHit)
        {
            Blink();
        }
    }

    void CheckIfTargetInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right, transform.right, fireRange);
        if (hit.transform == target && !isKnockedOut)
        {
            canFireToTarget = true;
            weapon.Attack();
        }
        else
        {
            canFireToTarget = false;
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + transform.right, transform.right * fireRange);
        if (!canFireToTarget)
        {
            Debug.DrawRay(transform.position + transform.right, transform.right * fireRange, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position + transform.right, transform.right * fireRange, Color.red);
        }
        
    }

    public void EnemyHit(HitEventData data)
    {
        StartCoroutine(BlinkingControl());
        StartCoroutine(KnockOut());
        hitPoints.TakeDamage(data.damage);
        rb.velocity = Vector2.zero;
        rb.AddForce(data.explosionForce);
    }

    IEnumerator KnockOut()
    {
        isKnockedOut = true;
        yield return new WaitForSeconds(1f);
        isKnockedOut = false;
    }

    void Blink()
    {
        colorIndex = (colorIndex + 1) % 2;

        spriteRenderer.color = colorList[colorIndex];
    }

    private IEnumerator BlinkingControl()
    {
        isHit = true;
        yield return new WaitForSeconds(2f);
        isHit = false;
        spriteRenderer.color = colorList[1];
    }

}
