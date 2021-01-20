using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonTrigger : MonoBehaviour {
    
    public BoxCollider2D PlayerCollider;

    
    public BoxCollider2D PlatformCollider;
    
    
    public BoxCollider2D PlatformTrigger;

	// Use this for initialization
	void Start () {
        PlayerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(PlatformCollider, PlatformTrigger, true);
	}
	
	// Update is called once per frame
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(PlatformCollider, PlayerCollider, true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(PlatformCollider, PlayerCollider, false);
        }
        }
}
