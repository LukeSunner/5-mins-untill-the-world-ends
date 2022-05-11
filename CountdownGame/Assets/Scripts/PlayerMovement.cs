using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Transform trans;
    
    private GameObject PlayerState;
    private GameObject Enemy;

    private UnityEngine.Vector3 respawnPoint;

    [SerializeField] private LayerMask ground;

    public float jumpPower;
    public float speed;
    
    private bool facingRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        
        
        
        PlayerState = GameObject.Find("GameManager");
        Enemy = GameObject.Find("Enemy");
        respawnPoint = trans.position;
    }

    
   private void Update()
   {
       float dirX = Input.GetAxisRaw("Horizontal");
       rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
       
       if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
       
       if (dirX >= 1 && facingRight == false) {
           Flip();

       }

       if (dirX <= -1 && facingRight)
       {
           Flip();
       }

       if (PlayerState.GetComponent<PlayerState>().HP <= 0)
       {
           trans.position = respawnPoint;
       }

       if (trans.position == respawnPoint)
       {
           PlayerState.GetComponent<PlayerState>().HP = 100;
       }
   }

   private bool IsGrounded()
   {
       return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, ground);
   }
   
   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.tag == "Pillar")
       {
           PlayerState.GetComponent<PlayerState>().HP -= 100;
           Score.scoreAmount = 0;
       }
    	
       if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
       {
           PlayerState.GetComponent<PlayerState>().HP -= 10;
       }

       if (collision.gameObject.tag == "Hitbox")
       {
           Enemy.GetComponent<EnemyMover>().isDead = true;
       }
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.tag == "Checkpoint")
       {
           respawnPoint = trans.position;
       }
   }

   void Flip()
   {
       facingRight = !facingRight;
       trans.Rotate(0f, 180f, 0f);
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
       switch (collision.name)
       {
           case "Potion 1" :
               Score.scoreAmount += 5;
               Destroy(collision.gameObject);
               break;
           case "Potion 2" :
               Score.scoreAmount += 10;
               Destroy(collision.gameObject);
               break;
           case "Potion 3" :
               Score.scoreAmount += 25;
               Destroy(collision.gameObject);
               break;
       }
   }
}
