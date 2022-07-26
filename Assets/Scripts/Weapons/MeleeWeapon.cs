using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{

    public Transform startPoint;
    public bool canAttack;
    public float damage = 20;

    public float attackTranslation = 1f;
    public float cooldownTime = 0.1f;
    private Collider2D weaponCollider;

    public float explosionForce = 500;
    private AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.transform;
        canAttack = false;

        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
        sounds = GetComponents<AudioSource>();
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    void Update()
    {

    }    
    
    public void Attack()
    {
        if(canAttack)
        {
            sounds[0].Play();
            transform.position += transform.right * attackTranslation;
            canAttack = false;
            weaponCollider.enabled = true;
            StartCoroutine(StopAttack());
            StartCoroutine(Cooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            RogueDicedEvents.hitEvent.Invoke(new HitEventData(collision.gameObject, gameObject, damage, transform.right * explosionForce));
            sounds[1].Play();
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = startPoint.position + startPoint.up * -0.3f;
        weaponCollider.enabled = false;
        
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
    }
}
