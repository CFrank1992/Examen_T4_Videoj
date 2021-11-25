using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFemaleController : MonoBehaviour
{

    //public properties
    public float velocityX = 2;
    public float jumpForce = 48;

    public float visionRadius;
    public float speed;

    public GameObject player;

    Vector3 initialPosition;

    //private components
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;


    //static properties

    //public bool playerDead = NinjaGirlController.isDead;

    //layers

    private const int LAYER_GROUND = 7;

    //tags
    private const string TAG_NINJAGIRL = "Ninja Girl";

    //bool states

    private bool isJumping = false;

    //Constrants

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_WALK = 1;
    private const int ANIMATION_DIE = 2;

    private float timeReaction = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
       


        sr.flipX = true;
        changeAnimation(ANIMATION_IDLE);

        timeReaction += Time.deltaTime;

        if(timeReaction == 3.5)
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            changeAnimation(ANIMATION_WALK);
        }
        else if(timeReaction == 7.5)
        {
            sr.flipX = false;
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            changeAnimation(ANIMATION_WALK);
        }
        else if(timeReaction == 12)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            changeAnimation(ANIMATION_IDLE);
            timeReaction = 0;
        }


/*
        if(MainController.countZombies == 10)
        {
            if(!isJumping)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                changeAnimation(ANIMATION_JUMP);
                isJumping = true;
            }
        }
        else if(MainController.countZombies == 20)
        {
            if(!isJumping && sr.flipX)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.velocity = new Vector2(-velocityX, rb.velocity.y);
                changeAnimation(ANIMATION_JUMP);
                isJumping = true;
                sr.flipX = false;
            }
            if(!isJumping && !sr.flipX)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rb.velocity = new Vector2(velocityX, rb.velocity.y);
                changeAnimation(ANIMATION_JUMP);
                isJumping = true;
                sr.flipX = true;
            }
        }*/
        
    }

   

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.layer == LAYER_GROUND)
        {
            isJumping = false;
        }    
    }






    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}