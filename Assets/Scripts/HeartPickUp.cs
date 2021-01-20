using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    private HealthManager healthManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthManager = FindObjectOfType<HealthManager>();
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null || other.tag == "sword" || other.tag == "Knife")
            return;
        healthManager.FullHealth();
        Destroy(gameObject);
    }
}
