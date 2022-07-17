using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Interface : MonoBehaviour
{

    public GameObject objectPoints;
    public GameObject objectHp;

    private TextMeshProUGUI textPoints;
    private TextMeshProUGUI textHp;

    public static int points;
    // Start is called before the first frame update
    void Start()
    {
        textPoints = objectPoints.GetComponent<TextMeshProUGUI>();
        textHp = objectHp.GetComponent<TextMeshProUGUI>();
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textPoints.text = "Score: " + Convert.ToInt32(points);
        textHp.text = "HP: " + Convert.ToInt32(PlayerMovement.hp);
    }

}
