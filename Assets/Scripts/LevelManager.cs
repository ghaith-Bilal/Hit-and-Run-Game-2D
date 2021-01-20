using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public int PointPenaltyOnDeath;
    public GameObject currentCheckPoint;
    private Player player;
    private HealthManager healthManager;
    public GameObject deathParticle;
    public GameObject respawnParticle;
    public float respawnDelay;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        healthManager = FindObjectOfType<HealthManager>();
        player.knockbackCount = 0;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

       public IEnumerator RespawnPlayerCo()
        {
            Instantiate(deathParticle, player.transform.position, player.transform.rotation);
            player.PlayerDeathAnimation(); 
            Debug.Log("Player Respawn");
            ScoreManager.AddPoints(-PointPenaltyOnDeath);
            //player.SwordCollider.enabled = false;
            player.enabled = false;
            player.myRigibody.drag = 5;
            //player.renderer.enabled = false;
            yield return new WaitForSeconds(respawnDelay);
            player.transform.position = currentCheckPoint.transform.position;
          
            player.PlayerRespawnAnimation();
            Instantiate(respawnParticle, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
            player.enabled = true;
            player.myRigibody.drag = 0;
            //player.renderer.enabled = true;
            healthManager.FullHealth();
            HealthManager.isDead = false;
          
            
        }
    
}
