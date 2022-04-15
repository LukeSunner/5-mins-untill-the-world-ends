using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public bool isDead;
    private GameObject MoveScript;
    void Start()
    {
        isDead = false;
        MoveScript = GameObject.Find("PlayerController");
    }

    
    void Update()
    {
        if (isDead == true)
        {
            MoveScript.GetComponent<AnimationScriptUsingAddForce>().enabled = false;
        }
    }
}
