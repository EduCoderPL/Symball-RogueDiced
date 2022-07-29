using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HitPoints))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private HitPoints hitPoints;

    [Header("Components")]
    [Tooltip("Audio Source of hit effect.")]
    public AudioSource hitEffectAudio;

    [Header("Movement info")]
    [Tooltip("The bigger it is - the faster the player is.")]
    public float moveSpeed = 5f;
    private Vector2 movement;

    [Header("Dodge info")]
    public TrailRenderer dodgeTrail;
    public float dodgeCooldownTime = 1f;
    public float dodgeDistance = 5f;
    private bool canDodge;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitPoints = GetComponent<HitPoints>();

        canDodge = true;
    }
    void Update()
    {
        PlayerInput();
        Dodge();
    }

    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void Dodge()
    {
        if (Input.GetMouseButtonDown(1) && canDodge)
        {
            dodgeTrail.emitting = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
            rb.velocity += dodgeDistance * lookDir;

            StartCoroutine(DodgeCooldown());
        }
    }

    IEnumerator DodgeCooldown()
    {
        canDodge = false;
        yield return new WaitForSeconds(0.1f);
        dodgeTrail.emitting = false;
        yield return new WaitForSeconds(dodgeCooldownTime);
        canDodge = true;

    }
    private void FixedUpdate()
    {
        rb.velocity += moveSpeed * Time.fixedDeltaTime * movement.normalized;
    }


    /// <summary>
    /// It makes damage effect for Player component HitPoints.
    /// </summary>
    /// <param name="data">HitEventData for enemy: victim, bullet, damage and vector of kickback force.</param>
    public void PlayerHit(HitEventData data)
    {
        rb.AddForce(data.explosionForce);
        hitPoints.TakeDamage(data.damage);
        hitEffectAudio.Play();
    }
}
