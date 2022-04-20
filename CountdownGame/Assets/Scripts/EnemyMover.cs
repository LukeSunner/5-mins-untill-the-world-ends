using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed, shootSpeed, range, reloadTime;
    private float distToPlayer;
    
    private Rigidbody2D enemy;
    private bool mustFlip, patrol, canShoot;
    private GameObject player;
    private Transform playerT;

    public GameObject bullet;
    public Transform shootPos;

    public bool isDead;
    private GameObject PlayerState;
    
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        isDead = false;
        player = GameObject.FindWithTag("Player");
        playerT = player.transform;
        patrol = true;
        canShoot = true;
        
        PlayerState = GameObject.Find("GameManager");
    }


    void Update()
    {
        if (PlayerState.GetComponent<PlayerState>().isDead == true)
        {
            canShoot = false;
        }
        
        if (patrol == true)
        {
            Patrol();
        } if (patrol == false)
        {
            
        }
       
        if (mustFlip == true)
        {
            Flip();
        }

        if (isDead == true)
        {
            Destroy(gameObject);
        }

        distToPlayer = Vector2.Distance(transform.position, playerT.position);

        if (distToPlayer <= range)
        {
            if (playerT.position.x > transform.position.x && transform.localScale.x < 0
                || playerT.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            patrol = false;
            enemy.velocity = Vector2.zero;
            if (canShoot == true)
            {
                StartCoroutine(Attack());
            }
        } else if (distToPlayer >= range)
        {
            patrol = true;
        }

    }

    void Patrol()
    {
        enemy.velocity = new Vector2(speed * Time.fixedDeltaTime, enemy.velocity.y);
    }

    void Flip()
    {
        
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {
            //mustFlip = true;
            Flip();
            print("shouldFlip");
        }
    }

    IEnumerator Attack()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2((shootSpeed * (Mathf.Abs(speed)/speed) * -1 * Time.fixedDeltaTime), 0f);
        canShoot = true;
    }
}
