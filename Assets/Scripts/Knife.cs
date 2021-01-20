using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour {
    [SerializeField]
    private float speed;
    public int damageToGive;
    private Rigidbody2D myRigidBody;
    private Vector2 direction;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = direction * speed;
    }
    public void Intialize (Vector2 direction)
    {
        this.direction = direction;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            Destroy(gameObject);
        }
    }
}
