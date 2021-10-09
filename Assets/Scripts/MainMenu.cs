using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Invocation: Called on click of 'Play' button
    //Use: Changes the current scene to the next scene (based on index)
    public void gameStart()
    {
        Debug.Log("Play btn pressed.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Invocation: Called on click of 'Quit' button
    //Use: Exits the application
    public void gameQuit()
    {
        Debug.Log("Quit btn pressed.");
        Application.Quit();
    }
}
