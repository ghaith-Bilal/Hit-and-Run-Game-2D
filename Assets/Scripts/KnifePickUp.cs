using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePickUp : MonoBehaviour {
    public int KnifesToAdd =10;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null || other.tag == "sword" || other.tag == "Knife")
            return;
        KnifesManager.AddKnifes(KnifesToAdd);        
        Destroy(gameObject);
    }
}
