using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    [SerializeField]
    private GameObject KnifeSet;
    [SerializeField]
    private GameObject Heart;
    [SerializeField]
    private GameObject Coin;
    public int x;
    [SerializeField]
    GameObject CurrentGameObject;
    public int SwordDamage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(SwordDamage);
        }
        if (other.tag == "Block" || other.tag == "Box")
        {

            Destroy(other.gameObject);
            if (other.tag == "Box")
            {
                x = Random.Range(0, (int) Time.realtimeSinceStartup);
                x = x % 3;
                if (x == 0)
                    CurrentGameObject = KnifeSet;
                if (x == 1)
                    CurrentGameObject = Heart;
                if (x == 2)
                    CurrentGameObject = Coin;

                CurrentGameObject = Instantiate(CurrentGameObject, other.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }
}
