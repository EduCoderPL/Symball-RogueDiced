using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

    public GameObject bulletPrefab;
    public bool isAutoFire;
    public float coolDown = 1f;
    private float lastTimeFire;
    public float bulletForce = 20f;
    private bool canFire;

    public bool isLaser;

    public LineRenderer laser;

    public float laserDistance = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        lastTimeFire = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (isAutoFire)
        {
            if (Input.GetButton("Fire1") && canFire)
            {
                Shoot();
            }
            else
            {
                if (laser)
                {
                    laser.enabled = false;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && canFire)
            {
                Shoot();
            }
        }


        if(Time.time > lastTimeFire + coolDown)
        {
            canFire = true;
        }
    }

    void Shoot()
    {
        if (!isLaser)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce);

            canFire = false;
            lastTimeFire = Time.time;
        }
        else
        {
            laser.enabled = true;
            laser.SetPosition(0, transform.position);
       
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserDistance);
            if (hit.collider != null)
            {
                laser.SetPosition(1, hit.point);
            }
            else
            {
                laser.SetPosition(1, transform.right * laserDistance);
            }
            
        }
        
    }
}
