using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour, Weapon
{

    public LineRenderer laser;

    public float laserDistance = 1000f;

    public float damagePerSecond = 60;

    public GameObject laserEnding;

    public AudioSource laserSound;



    // Start is called before the first frame update
    void Start()
    {
        laser.enabled = false;
        laserEnding.SetActive(false);
        laserSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
        else
        {
            laser.enabled = false;
            laserEnding.SetActive(false);
            laserSound.Stop();
        }
    }    
    
    public void Attack()
    {
        if (!laserSound.isPlaying)
        {
            laserSound.Play();
        }

        laser.enabled = true;
        laser.SetPosition(0, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserDistance);
        if (hit.collider != null)
        {
            laser.SetPosition(1, hit.point);
            if (hit.collider.transform.CompareTag("Enemy"))
            {
                EnemyMovement enemy = hit.collider.GetComponent<EnemyMovement>();
                enemy.hp -= damagePerSecond * Time.deltaTime;

            }

        }
        else
        {
            laser.SetPosition(1, transform.right * laserDistance);
        }
        laserEnding.SetActive(true);

        laserEnding.transform.position = laser.GetPosition(1);
    }
}
