using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator myAnimator;
    private Rigidbody2D myRigibody;
    private Transform myTransform; 

    public float moveSpeed;
    public bool moveRight;
    [SerializeField]
    private float TmpSpeed;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    
    private bool hittingWall;

    public bool playerSeen;

    private bool notAtEdge;
    public Transform edgeCheck;
    public Transform AttackA;
    public Transform AttackB;
    public Transform RunA;
    public Transform RunB;
    public Transform CrazyA;
    public Transform CrazyB;
    public Transform Behind;
    public LayerMask player;

    private EnemyHealthManager MonsterHealthManager;
    public float SpeedUp;
    public bool OnAnger;
    public float OnAngerHealth;
    public bool Crazy;
    public float CrazyHealth;
    private bool firstJump;
    public int JumpOnX,JumpOnY;
    public int Gear;
    public int CurrentGear;
    public bool CanMove;


    // Use this for initialization
    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        MonsterHealthManager = GetComponent<EnemyHealthManager>();
        OnAngerHealth = (MonsterHealthManager.EnemyHealth * 2)/3f;
        CrazyHealth = MonsterHealthManager.EnemyHealth / 3f;
        playerSeen = false;
        firstJump = true;
        TmpSpeed = moveSpeed;
        Gear = 0;
        CurrentGear = 0;

    }

    // Update is called once per frame
    void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);
        AbleToMove();
        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;
        
        if (CanMove)
        {
        if (moveRight)
            {
                myTransform.localScale = new Vector3(1f, 1f, 1f);
                myRigibody.velocity = new Vector2(moveSpeed, myRigibody.velocity.y);
            }
        else if(!moveRight)
            {
                myTransform.localScale = new Vector3(-1f, 1f, 1f);
                myRigibody.velocity = new Vector2(-moveSpeed, myRigibody.velocity.y);
            }
        }
        if (Physics2D.OverlapArea(AttackA.position, AttackB.position, player))
        {
            myAnimator.SetBool("monsterattack", true);
            playerSeen = true;
            Gear = 1;
            IncreaseSpeed();
        }
        if (Physics2D.OverlapArea(RunA.position, RunB.position, player))
        {
            if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Attack"))
                myAnimator.SetBool("monsterattack", false);
            myAnimator.SetFloat("monsterrun", 0.02f);
            playerSeen = true;
            Gear = 1;
            IncreaseSpeed();
        }
        if (!Physics2D.OverlapArea(RunA.position, RunB.position, player))
        {
            if (!OnAnger)
            {
                myAnimator.SetFloat("monsterrun", 0.0f);
                ResetSpeed();
            }
        }
        if (Physics2D.OverlapArea(Behind.position, RunB.position, player))
        {
            playerSeen = true;
            Flip();
        }
        if (!Physics2D.OverlapArea(AttackA.position, AttackB.position, player) && !Physics2D.OverlapArea(RunA.position, RunB.position, player))
        {
            myAnimator.SetBool("monsterattack", false);
            if(!OnAnger)
            {
                myAnimator.SetFloat("monsterrun", 0.0f);
                ResetSpeed();
            }
        }
        if (MonsterHealthManager.EnemyHealth <= OnAngerHealth)
        {
            Angry();
        }
        if (MonsterHealthManager.EnemyHealth <= CrazyHealth)
            Crazy = true;
        if (Physics2D.OverlapArea(CrazyA.position, CrazyB.position, player) && Crazy)
        {
            Gear = 3;
            IncreaseSpeed();
            CrazyJump();
        }
        if (Crazy && firstJump)
        {
            firstJump = false;
            Gear = 3;
            IncreaseSpeed();
            CrazyJump();
        }
        if (myAnimator.GetBool("monsterdead"))
        {
            myRigibody.velocity = Vector2.zero;
        }
    }
    public void IncreaseSpeed()
    {
        if (CurrentGear != Gear)
        {
            if (!OnAnger && !Crazy && Gear == 0)
                SpeedUp = 1f;
            else if (!OnAnger && !Crazy && Gear == 1)
                SpeedUp = 2f;
            else if (OnAnger && !Crazy && Gear == 2)
                SpeedUp = 4f;
            else if (OnAnger && Crazy && Gear == 3)
                SpeedUp = 6f;
        }
        else return;
        //TmpSpeed = moveSpeed;
        moveSpeed = SpeedUp * TmpSpeed;
        CurrentGear = Gear;
    }
    public void ResetSpeed()
    {
        if (!Crazy && !OnAnger && Gear == 1)
        {
            Gear = 0;
            SpeedUp = 1f;
            moveSpeed = SpeedUp *TmpSpeed;
            CurrentGear = Gear;
        }
    }
    public void Flip()
    {
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        moveRight = !moveRight;
    }
    public void Angry()
    {
        if(!Crazy)
        {
            Gear = 2;
            IncreaseSpeed();
        }
        myAnimator.ResetTrigger("monsterhurt");
        OnAnger = true;
    }
    public void CrazyJump()
    {
        if(!firstJump)
        myAnimator.SetTrigger("monsterjump");
    }
    public void Jump()
    {
        if (notAtEdge)
        {
            if (moveRight)
            {
                myRigibody.velocity = new Vector2(JumpOnX, JumpOnY);
                myTransform.localScale = new Vector3(1, 1, 1);
            }

            if (!moveRight)
            {
                myRigibody.velocity = new Vector2(-JumpOnX, JumpOnY);
                myTransform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public void AbleToMove()
    {
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Attack")
            && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Hurt")
            && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Jump")
            && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Monster_Dead")
            && playerSeen)
        {
            CanMove = true;
        }
        else CanMove = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "sword" || other.tag == "Knife") && !OnAnger && !Crazy)
            myAnimator.SetTrigger("monsterhurt");
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Knife")
    //    {
    //        myAnimator.SetTrigger("die");
    //        Destroy(gameObject);
    //    }

    //}

}
