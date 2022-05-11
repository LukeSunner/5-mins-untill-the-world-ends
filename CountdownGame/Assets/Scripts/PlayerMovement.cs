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
    private Animator anim;

    private UnityEngine.Vector3 respawnPoint;

    private GameObject canvas;
    private GameObject WinScreen;

    [SerializeField] private LayerMask ground;
    
    private enum MovementState {idle, running, jumping, falling}

    public float jumpPower;
    public float speed;
    private float dirX = 0f;
    
    private bool facingRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        
        
        
        PlayerState = GameObject.Find("GameManager");
        Enemy = GameObject.Find("Enemy");
        respawnPoint = trans.position;
        
        canvas = GameObject.Find("Canvas");
        WinScreen = canvas.transform.GetChild(5).gameObject;
    }

    
   private void Update()
   {
       dirX = Input.GetAxisRaw("Horizontal");
       rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

       if (dirX > 0)
       {
           
       }
       
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
       
       UpdateAnimationState();
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



   void Flip()
   {
       facingRight = !facingRight;
       trans.Rotate(0f, 180f, 0f);
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
       switch (collision.tag)
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
       if (collision.tag == "Checkpoint")
       {
           respawnPoint = trans.position;
       }

       if (collision.tag == "Finish")
       {
           WinScreen.SetActive(true);
       }
   }

   private void UpdateAnimationState()
   {
       MovementState state;
       if (dirX > 0f)
       {
           state = MovementState.running;
       }
       else if (dirX < 0f)
       {
           state = MovementState.running;
       }
       else
       {
           state = MovementState.idle;
       }

       if (rb.velocity.y > 0.1f)
       {
           state = MovementState.jumping;
       }

       if (rb.velocity.y < -0.1f)
       {
           state = MovementState.falling;
       }
       anim.SetInteger("state", (int)state);
   }
}
