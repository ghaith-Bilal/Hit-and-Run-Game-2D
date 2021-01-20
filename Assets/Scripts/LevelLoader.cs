using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private bool PlayerInZone;

    public string levelToLoad;
    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == "Project2")
            levelToLoad = "Level2";
        else if (Application.loadedLevelName == "Level2")
            levelToLoad = "Level3";
        else if (Application.loadedLevelName == "Level3")
            levelToLoad = "Level4";
        else if (Application.loadedLevelName == "Level4")
            levelToLoad = "SelectLevel";

        PlayerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && PlayerInZone)
        {
            Application.LoadLevel(levelToLoad);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            PlayerInZone = true;
}
   void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
            PlayerInZone = false;
    }
}
