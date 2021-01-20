using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStomper : MonoBehaviour {
    private Player player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
                other.GetComponent<EnemyHealthManager>().EnemyHurt.Play();
                player.knockbackCount = player.knockbackLength;

                if (transform.position.x < other.transform.position.x)
                {
                    player.knockfromRight = true;
                }
                else
                { 
                    player.knockfromRight = false;
                }
        }
    }
}
