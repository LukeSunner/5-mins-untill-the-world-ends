using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerState : MonoBehaviour
{
    public bool isDead;
    public int HP = 100;
    private GameObject MoveScript;
    
    GameObject canvas;
    GameObject varText;
    TextMeshProUGUI mText;
    private GameObject DeathScreen;
    void Start()
    {
        isDead = false;
        MoveScript = GameObject.Find("PlayerController");
        HP = 100;
        
        canvas = GameObject.Find("Canvas");
        varText = canvas.transform.GetChild(0).gameObject;
        mText = varText.GetComponent<TextMeshProUGUI>();
        DeathScreen = canvas.transform.GetChild(1).gameObject;
        DeathScreen.SetActive(false);
        mText.SetText("HP: " + HP);
    }

    
    void Update()
    {
        if (HP <= 0)
        {
            isDead = true;
            HP = 0;
        }
        
        if (isDead == true)
        {
            MoveScript.GetComponent<AnimationScriptUsingAddForce>().enabled = false;
            DeathScreen.SetActive(true);
        }
        
        mText.SetText("HP: " + HP);
    }
}
