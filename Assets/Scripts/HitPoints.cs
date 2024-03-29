using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{

    private float hp;
    [SerializeField] private float startHP = 100;
    [SerializeField] private IDeathEffect deathEffect;


    private void Start()
    {
        hp = startHP;
        TryGetComponent(out IDeathEffect outputComponent);
        deathEffect = outputComponent;
    }



    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        hp += healAmount;
        hp = Mathf.Clamp(hp, 0, startHP);
    }

    public void Die()
    {
        if(deathEffect != null)
        {
            deathEffect.Die();
        }
        else
        {
            if (transform.CompareTag("Enemy"))
            {
                RogueDicedEvents.killEnemyEvent.Invoke();
            }
            
            Destroy(gameObject);
        }
    }

    public float GetHP()
    {
        return hp;
    }
}
