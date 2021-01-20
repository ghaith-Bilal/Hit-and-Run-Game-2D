using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour {
    public int PointToAdd;
	// Use this for initialization
    public AudioSource coinSound;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.GetComponent<Player>() == null || other.tag == "sword" || other.tag == "Knife")
            return;
        ScoreManager.AddPoints(PointToAdd);

        coinSound.Play();

        Destroy(gameObject);
    }
}
