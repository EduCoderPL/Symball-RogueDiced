using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, Weapon
{

    public Transform startPoint;
    private bool isAttacked;
    public float damage = 20;

    public float attackTranslation = 1f;

    public float cooldownTime = 0.1f;

    private Collider2D weaponCollider;

    public float explosionForce = 500;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.transform;
        isAttacked = false;

        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacked)
        {
            Attack();
            
        }
        else if (isAttacked)
        {
            Invoke("Cooldown", cooldownTime);
        }
    }    
    
    public void Attack()
    {
        transform.position += transform.right * attackTranslation;
        isAttacked = true;
        weaponCollider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Hit by Halberd");
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            enemy.hp -= damage;

            Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
            enemyRB.AddForce(transform.right * explosionForce);

        }
    }

    void Cooldown()
    {
        isAttacked = false;
        transform.position = startPoint.position;
        weaponCollider.enabled = false;

    }
}
