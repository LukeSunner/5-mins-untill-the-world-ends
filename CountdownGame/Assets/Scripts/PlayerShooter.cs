using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform shootPos;
    public GameObject bulletPrefab;

    private float timer;

    public float reloadTime;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        timer += 1;
    }

    void Shoot()
    {
        if (Input.GetKeyDown("f"))
        {
            
            if (timer > reloadTime)
            {
                Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
                timer = 0;
            } else{}
        }
    }
}
