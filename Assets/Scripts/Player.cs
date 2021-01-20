using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public Rigidbody2D myRigibody;
    [SerializeField]
    private float movespeed;
    public float jumpHeight;
    public Transform KnifeSpawn;

    
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    
    [SerializeField]
    private GameObject knifePrefab;
    public BoxCollider2D headStomper;
    private SpriteRenderer PlayerSprite;
    public Animator myAnimator;

    private bool facingRight;
    private bool doubleJumped;

    public bool hit;
    private bool attack;
    // public bool jump;
    private bool slide;
    private bool glide;
    //private bool dead;

    public bool blinking;
    private float blinkingDuration;
    
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockfromRight;
    


    // Use this for initialization
    void Start()
    {
        facingRight = true;
        myRigibody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        PlayerSprite = GetComponent<SpriteRenderer>();
        blinkingDuration = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            slide = true;
        }
            HandleMovement(horizontal);
            Flip(horizontal);
            HandleLayer();
            
            ResetValues();
    }
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        HandleAttacks();
        if (hit && KnifesManager.Knifes != 0 && (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Throw") || !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("Jump_Throw")))
        {
            if(this.myAnimator.GetLayerWeight(1) == 0)
            {
                myAnimator.SetTrigger("hit");
            }
            else if (this.myAnimator.GetLayerWeight(1) == 1)
            {
                myAnimator.SetTrigger("jhit");
            }
            //ThrowKnife(); Called By Animation Window
        }
        if (attack && (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("Jump_Attack")))
        {
            if (this.myAnimator.GetLayerWeight(1) == 0)
            {
                myAnimator.SetTrigger("attack");
            }
            if (this.myAnimator.GetLayerWeight(1) == 1)
            {
                myAnimator.SetTrigger("jattack");
            }
        }
        //if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && this.myAnimator.GetLayerWeight(1) == 1)
        //{
        //    myAnimator.SetTrigger("jattack");
        //    myAnimator.ResetTrigger("attack");
        //}
        //if (this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("Jump_Attack") && this.myAnimator.GetLayerWeight(1) == 0)
        //{
        //    myAnimator.SetTrigger("attack");
        //    myAnimator.ResetTrigger("jattack");
        //}
        if (grounded)
        {
            doubleJumped = false;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("land", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            myRigibody.velocity = new Vector2(myRigibody.velocity.x, jumpHeight);
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !doubleJumped && !grounded)
        {
            myRigibody.velocity = new Vector2(myRigibody.velocity.x, jumpHeight);

            doubleJumped = true;

        }
        if (Input.GetKey(KeyCode.LeftControl) && !grounded)
        {
            myAnimator.SetFloat("glide", 0.02f);
            myAnimator.ResetTrigger("jump");
            myRigibody.gravityScale = 0.5f;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) || grounded)
        {
            myRigibody.gravityScale = 2;
            myAnimator.SetFloat("glide", 0.0f);
        }
        //ResetValues();
    }
    public void HandleAttacks()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hit = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            attack = true;
        }
    }
    private void HandleMovement(float horizontal)
    {
            if (myRigibody.velocity.y < 0)
            {
                myAnimator.SetBool("land", true);
            }
            if (knockbackCount <= 0)
            {
                myRigibody.velocity = new Vector2(horizontal * movespeed, myRigibody.velocity.y);
            }
            else
            {
                if (knockfromRight)
                    myRigibody.velocity = new Vector2(-knockback, knockback);

                if (!knockfromRight)
                    myRigibody.velocity = new Vector2(knockback, knockback);

                knockbackCount -= Time.deltaTime;
            }
            myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

            if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                myAnimator.SetBool("slide", true);
                movespeed += 10;
            }
            else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
            {
                myAnimator.SetBool("slide", false);
                movespeed = 10;
                
            }
            
        }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    public void ThrowKnife()
    {
        KnifesManager.AddKnifes(-1);
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, KnifeSpawn.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Knife>().Intialize(Vector2.right);
           
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(knifePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Knife>().Intialize(Vector2.left);
            
        }
        
    }

    public void ResetValues()
    {
        slide = false;
        
        hit = false;
        attack = false;
        
      //  jump = false;
    }
    private void HandleLayer()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
            myAnimator.SetLayerWeight(1, 0);
    }
    public void PlayerDeathAnimation()
    {
        myAnimator.SetBool("dead", true);
        //dead = true;
    }
    public void PlayerRespawnAnimation()
    {
        myAnimator.SetBool("dead", false);
        //dead = false;
    }
    public void Blink()
    {
        myAnimator.SetTrigger("damage");
        StartCoroutine(StartBlinking());
        
    }
    public IEnumerator StartBlinking()
    {
        blinking = true;
        StartCoroutine(Blinking());
        yield return new WaitForSeconds(blinkingDuration);
        blinking = false;
        myAnimator.ResetTrigger("damage");
    }
    public IEnumerator Blinking()
    {
        while (blinking)
        {
            //player.GetComponent<Animator>().SetBool("damage", true);
            PlayerSprite.enabled = false;
            yield return new WaitForSeconds(.1f);
            PlayerSprite.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
