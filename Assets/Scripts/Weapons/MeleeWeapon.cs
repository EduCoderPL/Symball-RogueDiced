using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, Weapon
{

    public Transform startPoint;
    public bool canAttack;
    public float damage = 20;

    public float attackTranslation = 1f;

    public float cooldownTime = 0.1f;

    private Collider2D weaponCollider;

    public float explosionForce = 500;
    private AudioSource slashSound;


    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.transform;
        canAttack = true;

        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
        slashSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Attack();
            
        }
    }    
    
    public void Attack()
    {
        transform.position += transform.right * attackTranslation;
        canAttack = false;
        weaponCollider.enabled = true;
        StartCoroutine(StopAttack());
        StartCoroutine(Cooldown());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            enemy.hp -= damage;

            Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
            enemyRB.AddForce(transform.right * explosionForce * (1 + enemyRB.velocity.magnitude/10));
            slashSound.Play();

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
