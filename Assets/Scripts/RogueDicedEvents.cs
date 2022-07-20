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
    public Vector2 explosionForce;

    public HitEventData(GameObject victim, GameObject projectile, float damage, Vector2 explosionForce)
    {
        this.victim = victim;
        this.projectile = projectile;
        this.damage = damage;
        this.explosionForce = explosionForce;
    }
}