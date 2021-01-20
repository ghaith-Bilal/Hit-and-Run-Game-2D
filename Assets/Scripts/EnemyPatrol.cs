using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public Animator myAnimator;
    private Rigidbody2D myRigibody;
    private Transform myTransform;

    public float moveSpeed;
    public bool moveRight;
    private float TmpSpeed;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    

	// Use this for initialization
	void Start () {
        myRigibody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
        TmpSpeed = moveSpeed;
	
	}
	
	// Update is called once per frame
	void Update () {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge =  Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);
        
        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;

        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"))
        {
            if (moveRight)
            {
                myTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                myRigibody.velocity = new Vector2(moveSpeed, myRigibody.velocity.y);

            }
            else
            {
                myTransform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
                myRigibody.velocity = new Vector2(-moveSpeed, myRigibody.velocity.y);
            }
        }
	}
    public void IncreaseSpeed()
    {
        if (moveSpeed > TmpSpeed)
            return;
        else
        {
            TmpSpeed = moveSpeed;
            moveSpeed *= 3f;
        }
    }
    public void ResetSpeed()
    {
        moveSpeed = TmpSpeed;
    }
    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        moveRight = !moveRight;
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
