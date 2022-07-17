using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    public static float hp = 100;

    public float kickBackForce = 1000;

    private bool canDodge;
    public float dodgeCooldownTime = 1f;

    Vector2 movement;

    private void Start()
    {
        canDodge = true;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (hp <= 0)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(transform.GetChild(0).GetComponent<SpriteRenderer>());
            StartCoroutine(BackToMenu());

        }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Vector2 kickBackVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(kickBackVector * kickBackForce);
            hp -= 10;
            GetComponent<AudioSource>().Play();
        }
    }


    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }



}
