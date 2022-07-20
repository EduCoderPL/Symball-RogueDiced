using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{

    private float hp;
    [SerializeField] private float startHP = 100;
    [SerializeField] public IDeathEffect deathEffect;

    private void Awake()
    {
        hp = startHP;
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

    public void Heal(float healAmoount)
    {
        hp += healAmoount;
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
            Destroy(gameObject);
        }
    }

    public float GetHP()
    {
        return hp;
    }
}
