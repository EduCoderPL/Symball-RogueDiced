using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    public List<GameObject> listOfWeapons;
    public Camera cam;

    public GameObject[] activeWeapons;

    public bool delayedRotation;
    Vector2 mousePos;

    public GameObject weapon;

    public float rotationSpeed = 50f;

    public static int[] numbers;
    public float timeToRandomize = 60f;
    // Start is called before the first frame update
    void Start()
    {
        numbers = new int[] { 1, 3 };
        StartCoroutine(RandomNumbersGo());


        activeWeapons[0] = listOfWeapons[numbers[0] - 1];
        activeWeapons[1] = listOfWeapons[numbers[1] - 1];

        setWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (numbers[0] != numbers[1])
        {
            if(Input.GetKeyDown("q")) setWeapon(0);
            if (Input.GetKeyDown("e")) setWeapon(1);
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
        GameObject temp = activeWeapons[number];
        weapon = Instantiate(temp, transform.position + transform.right * 0.4f + transform.up * -0.3f, transform.rotation);
        weapon.transform.SetParent(transform);
    }


    IEnumerator RandomNumbersGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToRandomize);
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                activeWeapons[i] = listOfWeapons[numbers[i] - 1];
            }
            setWeapon(Random.Range(0, 2));
            
        }

    }


}
