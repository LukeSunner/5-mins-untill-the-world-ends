using System;
using UnityEngine;
using System.Collections;

public class AnimationScriptUsingAddForce: MonoBehaviour
{
	
	public float maxSpeed;
	public float jumpPower;
	public float accelerationSpeed;//how fast the Rigidbody will accelerate to maxSpeed
    public float inAirAccelerationSpeed;
    public float inAirMaxSpeed;
    private GameObject PlayerState;
    private GameObject Enemy;
    private Transform mytransform;
    Rigidbody2D myrigidbody; //sets a variable called myrigidbody of type Rigidbody2D. not written as public so will be stored privately
	//Animator anim; //sets a variable called anim of type Animator
	float currentSpeed; //this will be used to check for the horizontal velocity of the Rigidbody2D
	float upSpeed;//this will be used to check for the vertical velocity of the Rigidbody2D
	//public Transform animatorTransform;
    public bool amIGrounded;

    public GameObject bullet;
    public Transform shootPos;
    public float shootSpeed;
    public float reloadTime;

    private bool facingRight = true;


   void Start ()
   {
    //anim = GetComponentInChildren<Animator>(); //getcomponent Animator and assigns it to anim
	myrigidbody = GetComponent<Rigidbody2D>(); //getcomponent Rigidbody2D and assigns it to myrigidbody
	PlayerState = GameObject.Find("GameManager");
	Enemy = GameObject.Find("Enemy");
	mytransform = GetComponent<Transform>();
   }

   private void Update()
   {
	   upSpeed = myrigidbody.velocity.y; //checks what the current vertical speed of the player is, and sets the float upSpeed to that value

	   if (Input.GetKeyDown("space") && amIGrounded == true) //checks if up button is being pressed, and if the rigidbody is not already moving up/down, and if the player is standing on something
	   {
		   myrigidbody.AddForce (new Vector2 (0, jumpPower)); //if all the above conditions are correct, then jumps
	   }

	   /*if (Input.GetKeyDown("f") == true)
	   {
		   StartCoroutine(Attack());
	   }*/
	   
   }

   void FixedUpdate () //change this to fixedupdate, better than update. Fixedupdate happens every physics calculation
   {




        //move code begins

        currentSpeed = myrigidbody.velocity.x; //sets currentSpeed variable to the current horizontal velocity of the rigidbody

    float move = Input.GetAxis ("Horizontal"); //checks input axis on the horizontal, so A, D or arrow left right buttons. will also work with a controller

		//this following if/else statement checks to see if the user pressed the right/left buttons, and tells the Animator if the right button has been pressed or the left
		if (currentSpeed > 0 && facingRight == false) {
			Flip();

		}

		if (currentSpeed < 0 && facingRight)
		{
			Flip();
		}

	


		if (Mathf.Abs(currentSpeed)<maxSpeed && amIGrounded == true){ //will only add more force if maxSpeed is not yet reached, AND player is on the ground. Mathf.Abs is used to make sure currentSpeed is always a positive value even if it is going left, which normally results in a negative value
			myrigidbody.AddForce(new Vector2 (move*accelerationSpeed, 0)); //this adds a force on the rigidbody of (move*accelerationSpeed).)
		}

        if (Mathf.Abs(currentSpeed) < inAirMaxSpeed && amIGrounded == false) // if player is in the air, reduce the amount of force added, since player is in air
        { 
            myrigidbody.AddForce(new Vector2(move * inAirAccelerationSpeed, 0)); //this adds a force on the rigidbody of (move*inAirAccelerationSpeed).)
        }

        //anim.SetFloat("speed", (Mathf.Abs(currentSpeed+move))); //checks the currentspeed and outputs the value as speed of the animator. move is added so if trying to move against a wall, it will still play animation, as it will detect the keypress. Mathf.Abs returns the absolute value (so if movement is negative, will still send a positive value)

		//move code ends



	//jump code starts

		float moveup = Input.GetAxis ("Vertical"); //checks input axis on the vertical so W, S, up and down arrow keys
	
		/*upSpeed = myrigidbody.velocity.y; //checks what the current vertical speed of the player is, and sets the float upSpeed to that value

		if (Input.GetKeyDown("space") && amIGrounded == true) //checks if up button is being pressed, and if the rigidbody is not already moving up/down, and if the player is standing on something
		{
			myrigidbody.AddForce (new Vector2 (0, jumpPower)); //if all the above conditions are correct, then jumps
		}*/


		//anim.SetFloat("verticalSpeed", (upSpeed + moveup)); //tells the animator what the current vertical speed of the player is, in the form of a float called verticalSpeed. if positive, is moving up, negative, is falling

	//jump code ends

   }

    //checks if player is on the ground, and sends a bool to the Animator called grounded that is true if the player is on the ground, false if not. Uses a trigger underneath the player that checks if it is inside a collider or not
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            amIGrounded = true;
            //anim.SetBool("grounded", true);
        }

        if (collision.gameObject.tag.Equals("Platform"))
        {
	        this.transform.parent = collision.transform;
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            amIGrounded = false;
            //anim.SetBool("grounded", false);
        }
        if (collision.gameObject.tag.Equals("Platform"))
        {
	        this.transform.parent = null;
        }

    }
    //end of groundcheck

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
    
    IEnumerator Attack()
    {
	    yield return new WaitForSeconds(reloadTime);
	    GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

	    newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2((shootSpeed * Mathf.Abs(myrigidbody.velocity.x/myrigidbody.velocity.x) * Time.fixedDeltaTime), 0f);
    }

    void Flip()
    {
	    facingRight = !facingRight;
	    mytransform.Rotate(0f, 180f, 0f);
    }
}