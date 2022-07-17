using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public List<GameObject> listOfWeapons;
    public Camera cam;

    public bool delayedRotation;
    Vector2 mousePos;

    public GameObject weapon;

    public float rotationSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        setWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < listOfWeapons.Count; i++)
        {
            string iString = "" + (i+1);
            if(Input.GetKeyDown(iString)) setWeapon(i);
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if (!delayedRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
        
    }

    void setWeapon(int number)
    {
        Destroy(weapon);
        GameObject temp = listOfWeapons[number];
        weapon = Instantiate(temp, transform.position + transform.right * 0.4f + transform.up * -0.3f, transform.rotation);
        weapon.transform.SetParent(transform);
        delayedRotation = number == 5;

    }

    
}
