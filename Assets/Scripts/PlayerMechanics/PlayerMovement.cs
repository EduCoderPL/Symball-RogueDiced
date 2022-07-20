using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public HitPoints hitPoints;

    public float kickBackForce = 1000;

    private bool canDodge;
    public float dodgeCooldownTime = 1f;

    Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitPoints = GetComponent<HitPoints>();

        canDodge = true;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetMouseButtonDown(1) && canDodge)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
            rb.AddForce(lookDir * 200f);
            StartCoroutine(DodgeCooldown());
        }

    }

    IEnumerator DodgeCooldown()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCooldownTime);
        canDodge = true;

    }
    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity += moveSpeed * Time.fixedDeltaTime * movement;
    }
    public void PlayerHit(HitEventData data)
    {
        rb.AddForce(data.explosionForce * kickBackForce);
        hitPoints.TakeDamage(10);
        GetComponent<AudioSource>().Play();
    }
}
