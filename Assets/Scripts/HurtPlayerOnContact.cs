using UnityEngine;
using System.Collections;


public class HurtPlayerOnContact : MonoBehaviour
{
	public int damageToGive;
    public float BounceOnX, BounceOnY;
    public GameObject hitParticle;

    private Player player;

   

	// Use this for initialization
	void Start ()
	{
        player = FindObjectOfType<Player>();
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
        if (other.tag == "Player")
        {
            if (!Physics2D.IsTouching(gameObject.GetComponent<BoxCollider2D>(), player.headStomper) && !other.GetComponent<Player>().blinking)
            {
                HealthManager.HurtPlayer(damageToGive);
                other.GetComponent<Player>().Blink();
                Instantiate(hitParticle, player.transform.position, player.transform.rotation);
                if(!other.GetComponent<Monster>() == false)
                {
                    if (GetComponentInParent<Monster>().moveRight)
                        player.myRigibody.velocity = new Vector2(BounceOnX, BounceOnY);
                    else if (!GetComponentInParent<Monster>().moveRight)
                        player.myRigibody.velocity = new Vector2(BounceOnX, BounceOnY);
                }
            }
            else
            {
                //gameObject.GetComponent<EnemyHealthManager>().EnemyHurt.Play();
                player.knockbackCount = player.knockbackLength;

                if (other.transform.position.x < transform.position.x)
                    player.knockfromRight = true;
                else
                    player.knockfromRight = false;
            }
            
        }
	}
}
