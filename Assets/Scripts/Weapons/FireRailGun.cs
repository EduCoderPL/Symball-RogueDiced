using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRailGun : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    public bool isAutoFire;
    public float coolDown = 1f;
    private float lastTimeFire;
    public float bulletForce = 20f;
    private bool canFire;

    private Transform endOfBarrel;
    private EnemyRangedMovement weaponUser;

    public LineRenderer laser;
    public float laserDistance = 20f;

    public float kickBackForce = 5000f;
    public float damage;





    // Start is called before the first frame update
    void Start()
    {
        canFire = false;
        lastTimeFire = Time.time;
        endOfBarrel = transform.GetChild(0);
        weaponUser = transform.parent.GetComponent<EnemyRangedMovement>();
        laser.startWidth = 0.05f;
    }


    // Update is called once per frame
    void Update()
    {
        if (!canFire && Time.time > lastTimeFire + coolDown)
        {
            if (isAutoFire || !isAutoFire && Input.GetButtonUp("Fire1"))
                canFire = true;
        }

        laser.SetPosition(0, endOfBarrel.position);
        RaycastHit2D hit = Physics2D.Raycast(endOfBarrel.position, transform.right, laserDistance);
        if (hit.collider != null && false)
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, transform.position + transform.right * laserDistance);
        }

    }

    public void Attack()
    {
        if (canFire)
        {
            laser.startWidth = 0.1f;
            weaponUser.isAiming = true;
            canFire = false;
            lastTimeFire = Time.time;
            StartCoroutine(FireBullet());
        }
    }

    IEnumerator FireBullet()
    {
        yield return new WaitForSeconds(0.7f);
        laser.startWidth = 0.2f;

        RaycastHit2D tempHit = Physics2D.Raycast(endOfBarrel.position, endOfBarrel.right, laserDistance);
        if (tempHit.collider != null)
        {
            laser.SetPosition(1, tempHit.point);
            if (tempHit.collider.transform.CompareTag("Enemy") || tempHit.collider.transform.CompareTag("Player"))
            {
                RogueDicedEvents.hitEvent.Invoke(new HitEventData(tempHit.collider.gameObject, null, damage, kickBackForce * transform.right));
            }
        }
            lastTimeFire = Time.time;
        yield return new WaitForSeconds(0.3f);
        laser.startWidth = 0.05f;
        weaponUser.isAiming = false;
    }
}
