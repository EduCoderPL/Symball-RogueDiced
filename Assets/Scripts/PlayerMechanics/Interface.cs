using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{

    public GameObject objectPoints;
    public GameObject objectHp;
    public GameObject objectDiceText;

    public Sprite[] diceImages;
    public Image[] actualDicesImage;


    private TextMeshProUGUI textPoints;
    private TextMeshProUGUI textHp;
    private TextMeshProUGUI textDice;

    public GameObject player;
    private float playerHP;


    private float estimatedTimeToRoll;


    public static int points;
    // Start is called before the first frame update


    void Awake()
    {
        RogueDicedEvents.rollDice.AddListener(SetImages);
        RogueDicedEvents.killEnemyEvent.AddListener(AddPoints);
        RogueDicedEvents.rollDice.Invoke();
        textPoints = objectPoints.GetComponent<TextMeshProUGUI>();
        textHp = objectHp.GetComponent<TextMeshProUGUI>();
        textDice = objectDiceText.GetComponent<TextMeshProUGUI>();
        points = 0;
        playerHP = player.GetComponent<HitPoints>().GetHP();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        textPoints.text = "Score: " + Convert.ToInt32(points);
        textHp.text = "HP: " + Convert.ToInt32(player ? player.GetComponent<HitPoints>().GetHP(): 0);

        textDice.text = "TIME TO NEXT ROLL: \n" + Mathf.Ceil(estimatedTimeToRoll - Time.time);

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    

    public void SetImages()
    {
        for (int i = 0; i<2; i++)
        {
            actualDicesImage[i].sprite = diceImages[WeaponAiming.numbers[i] - 1];
        }
        estimatedTimeToRoll = Time.time + WeaponAiming.timeToRandomize;
        objectDiceText.SetActive(false);

    }

    public void AddPoints()
    {
        points += 100;
    }


}
