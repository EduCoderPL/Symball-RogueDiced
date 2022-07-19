using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class RogueDicedEvents
{
    public static UnityEvent rollDice = new();
    public static HitEvent hitEvent = new HitEvent();
}

public class HitEvent : UnityEvent<HitEventData> { }

public class HitEventData
{
    public GameObject victim;
    public GameObject projectile;
    public float damage;
    public float explosionForce;

    public HitEventData(GameObject victim, GameObject projectile, float damage, float explosionForce)
    {
        this.victim = victim;
        this.projectile = projectile;
        this.damage = damage;
        this.explosionForce = explosionForce;
    }
}