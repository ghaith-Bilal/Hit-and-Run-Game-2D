using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{   
    public static int playerHealth;
	public int maxPlayerHealth;
	Text text;
    public static bool isDead;
    private Player player;

	public LevelManager levelManager;

    

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<Text> ();
        playerHealth = maxPlayerHealth;
		levelManager = FindObjectOfType<LevelManager> ();
		isDead = false;
        player = FindObjectOfType<Player>();   
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
			isDead = true;
            player.myRigibody.velocity = Vector2.zero;
            levelManager.RespawnPlayer();
		}

		text.text = " " + playerHealth;
	}

	public static void HurtPlayer (int damageToGive)
	{
        playerHealth -= damageToGive;
	}

	public  void FullHealth ()
	{
        playerHealth = maxPlayerHealth;
	}  
}
