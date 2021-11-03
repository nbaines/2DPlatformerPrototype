using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject persister;
    public LevelLoader loader;
    private bool playPressed = false;
    public void Awake()
    {

        loader = GameObject.FindGameObjectWithTag("Persistance").GetComponent<LevelLoader>();
    }

    //Invocation: Called on click of 'Play' button
    //Use: Changes the current scene to the next scene (based on index)
    public void gameStart()
    {

        Debug.Log("Play button pressed. Starting game.");
        if (!playPressed)
        {
            loader.LoadNextLevel();    //changed this to make the level loader script handle loading from everwhere.
            playPressed = true;
        }
    }

    //Invocation: Called on click of 'Quit' button
    //Use: Exits the application
    public void gameQuit()
    {
        Debug.Log("Quit button pressed. Exiting game.");
        Application.Quit();
    }
}
