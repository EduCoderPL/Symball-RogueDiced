using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class RogueDicedEvents
{
    public static RollDiceEvent rollDiceEvent = new();
    public static HitEvent hitEvent = new();
    public static UnityEvent killEnemyEvent  = new();
    public static UnityEvent rollInfoEvent = new();
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

public class KillEventData
{
    public GameObject victim;

    public KillEventData(GameObject victim)
    {
        this.victim = victim;
    }
}

public class RollDiceEvent : UnityEvent<RollDiceEventData> { }

public class RollDiceEventData
{
    public int[] weaponsNumbers;

    public RollDiceEventData(int[] weapons)
    {
        this.weaponsNumbers = weapons;
    }
}