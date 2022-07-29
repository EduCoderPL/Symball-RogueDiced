using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    [Header("Weapons management")]
    [Tooltip("List of all weapons that player can use")]
    [SerializeField] List<GameObject> listOfWeapons;

    [Tooltip("Two of active weapons.")]
    [SerializeField] GameObject[] activeWeapons;

    [Tooltip("Numbers of active weapons.")]
    public static int[] numbers;

    [Tooltip("Randomize weapon cooldown.")]
    public static float timeToRandomize = 15f;

    [Header("Weapon actually used by player")]
    [Tooltip("Gameobject instance of player`s weapon.")]
    public GameObject weapon;

    [Tooltip("Script of weapon.")]
    private IWeapon actualWeapon;

    [Tooltip("If false: it will have instant rotation.")]
    [SerializeField] bool delayedRotation;

    [Tooltip("If delayed rotation: speed of rotation in degress per second.")]
    [SerializeField] float rotationSpeed = 50f;

    [Tooltip("Info about Camera.")]
    private Camera cam;

    [Tooltip("Mouse Position")]
    private Vector2 mousePos;


    [SerializeField] Interface ui;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        numbers = new int[] { 1, 3 };
        StartCoroutine(RandomNumbersGo());


        activeWeapons[0] = listOfWeapons[numbers[0] - 1];
        activeWeapons[1] = listOfWeapons[numbers[1] - 1];

        SetWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        HandleInput();

        if (Input.GetButton("Fire1"))
        {
            actualWeapon.Attack();
        }
    }

    private void HandleInput()
    {
        if (numbers[0] != numbers[1])
        {
            if (Input.GetKeyDown(KeyCode.Q)) SetWeapon(0);
            if (Input.GetKeyDown(KeyCode.E)) SetWeapon(1);
        }
    }

    private void FixedUpdate()
    {
        RotateWeapon();

    }

    private void RotateWeapon()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;
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

    void SetWeapon(int number)
    {
        Destroy(weapon);
        GameObject temp = activeWeapons[number];
        weapon = Instantiate(temp, transform.position + transform.right * 0.4f + transform.up * -0.3f, transform.rotation);
        weapon.transform.SetParent(transform);
        actualWeapon = weapon.GetComponent<IWeapon>();
    }


    IEnumerator RandomNumbersGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToRandomize - 3);
            ui.objectDiceText.SetActive(true);
            yield return new WaitForSeconds(3);

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = Random.Range(1, 7);
                activeWeapons[i] = listOfWeapons[numbers[i] - 1];
            }
            RogueDicedEvents.rollDice.Invoke();
            SetWeapon(Random.Range(0, 2));

        }

    }


}
