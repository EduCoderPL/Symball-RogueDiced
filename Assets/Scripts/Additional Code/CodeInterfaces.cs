using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack();
}

public interface IDeathEffect
{
    void Die();
}

public interface IEnemy
{
    public void EnemyHit(HitEventData data);
}


