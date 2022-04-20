using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float dieTime, damage;
    void Start()
    {
        StartCoroutine(Timer());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        
        Destroy(gameObject);
    }
}
