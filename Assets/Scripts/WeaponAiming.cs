using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public List<GameObject> listOfWeapons;
    public Camera cam;
    Vector2 mousePos;

    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        setWeapon(0);

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKeyDown("1")) setWeapon(0);
        if (Input.GetKeyDown("2")) setWeapon(1);
        if (Input.GetKeyDown("3")) setWeapon(2);
        if (Input.GetKeyDown("4")) setWeapon(3);
        if (Input.GetKeyDown("5")) setWeapon(4);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void setWeapon(int number)
    {
        Destroy(weapon);
        GameObject temp = listOfWeapons[number];
        weapon = Instantiate(temp, transform.position + transform.right * 0.4f, transform.rotation);
        weapon.transform.SetParent(transform);

    }
}
