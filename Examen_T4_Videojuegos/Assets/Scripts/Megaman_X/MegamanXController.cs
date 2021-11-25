using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MegamanXController : MonoBehaviour
{
    //public properties
    public float velocityX = 12f;
    public float velocitySlideX = 20f;
    
    public float jumpForce = 40f;

    public GameObject balaDerecha1;
    public GameObject balaIzquierda1;

    public GameObject balaDerecha2;
    public GameObject balaIzquierda2;

    public GameObject balaDerecha3;
    public GameObject balaIzquierda3;


    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;


    private float chargeTime = 0;

    private bool isJumping = false;
    private bool isDead = false;


    public AudioClip[] audioClips;

    private AudioSource _audioSource;

    //Constants

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_SLIDE = 3;
    private const int ANIMATION_SHOOT = 4;
    private const int ANIMATION_RUNSHOOT = 5;
    private const int ANIMATION_DEAD = 6;

    private const int ANIMATION_LOW_HEALTH = 10;


    //Tags

    private const string TAG_PISO = "Ground";
    private const string TAG_KEY = "Key";
    private const string TAG_ENEMY = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quieto
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);

        //caminarDerecha
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);

        }

        //caminarIzquierda
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
            
        }

        //Saltar
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(ANIMATION_JUMP);
            _audioSource.PlayOneShot(audioClips[7]);
            isJumping= true;
        }

        //deslizar
        if(Input.GetKeyDown(KeyCode.Z))
        {
            _audioSource.PlayOneShot(audioClips[6]);
            if(sr.flipX == false)
            {
                rb.velocity = new Vector2(velocitySlideX, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-velocitySlideX, rb.velocity.y);
            }
            
            
            changeAnimation(ANIMATION_SLIDE);
        }

        //cargaDisparo
        if(Input.GetKey(KeyCode.X))
        {
            Debug.Log("Cargando...");
            chargeTime += Time.deltaTime;

            if(chargeTime > 0.0 && chargeTime < 1.5)
            {
                sr.color = new Color(1,1,1,1);
                
            }
            else if(chargeTime > 1.5 && chargeTime < 2.5)
            {
                sr.color = new Color(0,1,0,1);
                
            }
            else if(chargeTime > 2.5)
            {
                sr.color = new Color(1,1,0,1);
                
            }
        }

        //Disparar
        if(Input.GetKeyUp(KeyCode.X))
        {
            if(chargeTime < 0.5)
            {
                //Crear el objeto
                //1. GameObject que debemos crear
                //2. Position donde va a aparecer
                //3. Rotación
                Debug.Log("Paso menos de 0.5 segundo, disparar bala simple");
                changeAnimation(ANIMATION_SHOOT);
                _audioSource.PlayOneShot(audioClips[0]);

                var bala = sr.flipX ? balaIzquierda1 : balaDerecha1;
                var position = new Vector2(transform.position.x,transform.position.y);
                var rotation = balaDerecha1.transform.rotation;
                Instantiate(bala,position,rotation);
            }
            else if(chargeTime < 1.5)
            {
                
                //Crear el objeto
                //1. GameObject que debemos crear
                //2. Position donde va a aparecer
                //3. Rotación
                Debug.Log("Paso menos de 1.5 segundo, disparar bala 2");
                changeAnimation(ANIMATION_SHOOT);
                _audioSource.PlayOneShot(audioClips[1]);
            
                var bala2 = sr.flipX ? balaIzquierda2 : balaDerecha2;
                var position = new Vector2(transform.position.x,transform.position.y);
                var rotation = balaDerecha2.transform.rotation;
                Instantiate(bala2,position,rotation);
            }
            else if(chargeTime < 2.5)
            {
                //Crear el objeto
                //1. GameObject que debemos crear
                //2. Position donde va a aparecer
                //3. Rotación
                Debug.Log("Paso menos de 2.5 segundo, disparar bala 3");
                changeAnimation(ANIMATION_SHOOT);
                _audioSource.PlayOneShot(audioClips[2]);
            
                var bala3 = sr.flipX ? balaIzquierda3 : balaDerecha3;
                var position = new Vector2(transform.position.x,transform.position.y);
                var rotation = balaDerecha3.transform.rotation;
                Instantiate(bala3,position,rotation);
            }

            chargeTime = 0;

        }

        


    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == TAG_PISO)
        {
            isJumping = false;
            
        }

        if(collision.gameObject.tag == TAG_ENEMY)
        {
            deadMegamanX();
            
        }

        if(collision.gameObject.tag == TAG_KEY)
        {
            SceneManager.LoadScene("Scene2");
        }

    }

    private void deadMegamanX()
    {
        if(!isDead)
        {
            velocityX = 0;
            changeAnimation(ANIMATION_DEAD);
        }
        isDead = true;
    }


    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
