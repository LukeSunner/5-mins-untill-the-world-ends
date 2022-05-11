using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerState : MonoBehaviour
{
    public bool isDead;
    public int HP = 100;
    public int lives = 3;
    private GameObject MoveScript;

    private Vector3 respawnPoint;
    
    GameObject canvas;
    GameObject varText;
    TextMeshProUGUI mText;
    private GameObject DeathScreen;
    private GameObject Hearts;
    private GameObject Heart2;
    private GameObject Heart1;
    private GameObject Heart;

    /* public GameObject fullHealth;
     public GameObject health90;
     public GameObject health80;
     public GameObject health70;
     public GameObject health60;
     public GameObject health50;
     public GameObject health40;
     public GameObject health30;
     public GameObject health20;
     public GameObject health10; */
    void Start()
    {
        isDead = false;
        MoveScript = GameObject.Find("PlayerController");
        HP = 100;

        respawnPoint = MoveScript.transform.position;
        
        canvas = GameObject.Find("Canvas");
        varText = canvas.transform.GetChild(0).gameObject;
        mText = varText.GetComponent<TextMeshProUGUI>();
        DeathScreen = canvas.transform.GetChild(1).gameObject;
        Hearts = canvas.transform.GetChild(2).gameObject;
        Heart2 = Hearts.transform.GetChild(2).gameObject;
        Heart1 = Hearts.transform.GetChild(1).gameObject;
        Heart = Hearts.transform.GetChild(0).gameObject;
        DeathScreen.SetActive(false);
        mText.SetText("HP: " + HP);
    }
    
    

    
    void Update()
    {
        print(lives);
        if (lives >= 3)
        {
            Heart.SetActive(true);
            Heart1.SetActive(true);
            Heart2.SetActive(true);
        }
        if (lives <= 2)
        {
            Heart2.SetActive(false);
        }

        if (lives <= 1)
        {
            Heart1.SetActive(false);
        }

        if (lives <= 0)
        {
            Heart.SetActive(false);
            MoveScript.GetComponent<PlayerMovement>().enabled = false;
            MoveScript.GetComponent<PlayerShooter>().enabled = false;
            DeathScreen.SetActive(true);
        }
        
        if (HP > 0)
        {
            isDead = false;
        }
        
        if (HP <= 0)
        {
           /* fullHealth.SetActive(false);
            health10.SetActive(false);
            health20.SetActive(false);
            health30.SetActive(false);
            health40.SetActive(false);
            health50.SetActive(false);
            health60.SetActive(false);
            health70.SetActive(false);
            health80.SetActive(false);
            health90.SetActive(false); */
            isDead = true;
            HP = 0;
            lives -= 1;
        }

        /* if (HP <= 10)
         {
             fullHealth.SetActive(false);
             health20.SetActive(false);
             health30.SetActive(false);
             health40.SetActive(false);
             health50.SetActive(false);
             health60.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 20)
         {
             fullHealth.SetActive(false);
             health30.SetActive(false);
             health40.SetActive(false);
             health50.SetActive(false);
             health60.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 30)
         {
             fullHealth.SetActive(false);
             health40.SetActive(false);
             health50.SetActive(false);
             health60.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 40)
         {
             fullHealth.SetActive(false);
             health50.SetActive(false);
             health60.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 50)
         {
             fullHealth.SetActive(false);
             health60.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 60)
         {
             fullHealth.SetActive(false);
             health70.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 70)
         {
             fullHealth.SetActive(false);
             health80.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 80)
         {
             fullHealth.SetActive(false);
             health90.SetActive(false);
         }
         
         if (HP <= 90)
         {
             fullHealth.SetActive(false);
         } */
        
       /* if (isDead == true)
        {
            MoveScript.GetComponent<PlayerMovement>().enabled = false;
            MoveScript.GetComponent<PlayerShooter>().enabled = false;
            DeathScreen.SetActive(true);
        } */
       
       mText.SetText("HP: " + HP);
    }

  
}
