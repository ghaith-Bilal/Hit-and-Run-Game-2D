using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour {
    private Animator EnemyAnimator;
    public Transform AttackA;
    public Transform AttackB;
    public Transform RunA;
    public Transform RunB;
    public Transform Behind;
    public LayerMask player;
    private EnemyPatrol thisEnemy;
	// Use this for initialization
	void Start () {
       
        EnemyAnimator = GetComponent<Animator>();
        thisEnemy = GetComponent<EnemyPatrol>();
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapArea(AttackA.position, AttackB.position, player))
        {
            EnemyAnimator.SetBool("attackenemy",true);
            EnemyAnimator.SetFloat("runenemy", 0.0f);
        }
        if (Physics2D.OverlapArea(RunA.position, RunB.position, player) && !Physics2D.OverlapArea(AttackA.position, AttackB.position, player))
        {
            if (this.EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("AttackEnemy"))
            {
                EnemyAnimator.SetBool("attackenemy", false);
            }
                EnemyAnimator.SetFloat("runenemy", 0.02f);
                thisEnemy.IncreaseSpeed();
        }
        if (!Physics2D.OverlapArea(RunA.position, RunB.position, player))
        {
            EnemyAnimator.SetFloat("runenemy", 0.0f);
            thisEnemy.ResetSpeed();
        }
        if (!Physics2D.OverlapArea(AttackA.position, AttackB.position, player) && !Physics2D.OverlapArea(RunA.position, RunB.position, player))
        {
            EnemyAnimator.SetBool("attackenemy", false);
            EnemyAnimator.SetFloat("runenemy", 0.0f);
        }
        if (Physics2D.OverlapArea(Behind.position, RunB.position, player))
        {
            thisEnemy.Flip();
        }

    }



}
