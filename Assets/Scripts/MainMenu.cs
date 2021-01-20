using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string startLevel;

    public string levelSelect;

    public string KeysInterface;


    public void NewGame ()
    {
        Application.LoadLevel(startLevel);
    }
    public void LevelSelect()
    {
        Application.LoadLevel(levelSelect);
    }
    public void Buttons()
    {
        Application.LoadLevel(KeysInterface);
    }

    public void QuitGame ()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }
	
}
