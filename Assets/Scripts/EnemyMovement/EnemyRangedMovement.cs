using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedMovement : MonoBehaviour, IEnemy
{

    public Transform target;
    public float enemySpeed = 0.1f;

    public float fireRange = 3f;

    private Rigidbody2D rb;
    public HitPoints hitPoints;

    private bool canFireToTarget;

    public IWeapon weapon;

    private bool isKnockedOut;

    private bool isHit;

    private Color[] colorList;
    private int colorIndex;

    public float rotationSpeed = 40f;

    public bool isAiming;


    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        canFireToTarget = false;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<IWeapon>();
        isKnockedOut = false;
        isHit = false;

        colorList = new Color[] { Color.black, spriteRenderer.color };
        colorIndex = 0;
        isAiming = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !isKnockedOut)
        {
            CheckIfTargetInRange();
            if (!isAiming)
            {
                Move();
            }
            

        }
        if (isHit)
        {
            Blink();
        }
    }

    public void Move()
    {

        Vector2 lookDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);

        if (!canFireToTarget)
        {
            rb.AddForce(enemySpeed * Time.deltaTime * lookDir);
        }

        else
        {
            rb.AddForce(-enemySpeed * Time.deltaTime * lookDir);
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
