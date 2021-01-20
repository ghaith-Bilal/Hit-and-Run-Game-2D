using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int EnemyHealth;
    //public int bounce;
    //public GameObject deathEfect;
    public AudioSource EnemyHurt;
    //public int SwordDamage;
    public GameObject hitParticle;
    public GameObject deathParticle;
    public GameObject coin;
    public GameObject NextLevel;
    //private Rigidbody2D bounceRB;
    public int pointsOnDeath;

    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyHealth <= 0)
        {
            if (GetComponent<Monster>()== null)
            {
                ScoreManager.AddPoints(pointsOnDeath);
                Instantiate(deathParticle, transform.position, transform.rotation);
                Instantiate(coin, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if (GetComponent<Monster>())
            {
                
                MonsterDeath();
            }
        }
    }
    public void giveDamage(int damageToGive)
    {
        
        Instantiate(hitParticle, new Vector3((transform.position.x) + 1f, transform.position.y, transform.position.z), transform.rotation);
        EnemyHurt.Play();
        EnemyHealth -= damageToGive;   
    }
    public void MonsterDeath()
    {
        GetComponent<Monster>().myAnimator.SetBool("monsterdead", true);
        StartCoroutine("MonsterDeathCo");
    }
     public IEnumerator MonsterDeathCo()
    {
        yield return new WaitForSeconds(3f);
        ScoreManager.AddPoints(pointsOnDeath);
        Instantiate(deathParticle, transform.position, transform.rotation);
        Instantiate(NextLevel, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "sword")
    //    {
    //        bounceRB.velocity = new Vector2(bounceRB.velocity.x, bounce);
    //        Instantiate(hitParticle, new Vector3((transform.position.x) + 1f, transform.position.y, transform.position.z), transform.rotation);
    //        EnemyHurt.Play();
    //        //giveDamage (SwordDamage);
    //    }
    //}

}