using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed, shootSpeed, range, reloadTime, HP;
    private float distToPlayer;
    
    private Rigidbody2D enemy;
    private bool mustFlip, patrol, canShoot, facingRight = true;
    private GameObject player;
    private Transform playerT;

    public GameObject bullet;
    public Transform shootPos;

    public bool isDead;
    public bool enemyShooter;
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
        HP = 10;
    }


    void Update()
    {
        if (HP <= 0)
        {
            isDead = true;
        }
        
        distToPlayer = Vector2.Distance(transform.position, playerT.position);
        
        if (enemyShooter == true)
        {
            if (PlayerState.GetComponent<PlayerState>().isDead == true)
            {
                canShoot = false;
            }

            if (patrol == true)
            {
                Patrol();
            }

            if (patrol == false)
            {

            }

            if (mustFlip == true)
            {
                Flip();
            }

            

            

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
            }
            else if (distToPlayer >= range)
            {
                patrol = true;
            }
        }

        if (enemyShooter == false)
        {
            Patrol();
            if (distToPlayer <= range)
            {
                if (playerT.position.x > transform.position.x && !facingRight)
                {
                    Flip();
                }

                if (playerT.position.x < transform.position.x && facingRight)
                {
                    Flip();
                }
            }
            
        }
        
        if (isDead == true)
        {
            Destroy(gameObject);
        }
    }

    void Patrol()
    {
        enemy.velocity = new Vector2(speed * Time.fixedDeltaTime, enemy.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            HP -= 5;
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
