using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour, IWeapon
{

    public GameObject bulletPrefab;
    public bool isAutoFire;
    public float coolDown = 1f;
    private float lastTimeFire;
    public float bulletForce = 20f;
    private bool canFire;

    private Transform endOfBarrel;



    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        lastTimeFire = Time.time;
        endOfBarrel = transform.GetChild(0);
    }


    // Update is called once per frame
    void Update()
    {
        if(!canFire && Time.time > lastTimeFire + coolDown)
        {
            if(isAutoFire || !isAutoFire && Input.GetButtonUp("Fire1"))
                canFire = true;
        }

    }


    public void Attack()
    {
        if (canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, endOfBarrel.position, endOfBarrel.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(endOfBarrel.right * bulletForce);

            canFire = false;
            lastTimeFire = Time.time;
        }
    }
}
