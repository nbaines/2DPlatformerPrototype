using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;

    // Update is called once per frame
    void Update()
    {

    }

    //Invocation: Called on click of 'Restart' button (in death menu)
    //Use: Restarts the current scene
    public void gameRestart()
    {
        deathMenuUI.SetActive(false); //disable death menu & child elements
        Time.timeScale = 1f; //standard timescale

        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.buildIndex); //restart current scene
    }

    //Invocation: Called when Player HP is fully depleted (i.e. <= 0)
    //Use: Stops gameplay, opens death menu
    public void gamePause()
    {
        Debug.Log("Player was killed.");

        deathMenuUI.SetActive(true); //enable death menu & child elements
        Time.timeScale = 0f; //pause time
    }

    //Invocation: Called on click of 'Main Menu' button (in death menu)
    //Use: Returns player to the main menu
    public void gameReturnMainMenu()
    {
        Time.timeScale = 1f; //standard timescale

        Debug.Log("Main Menu button pressed. Returning player to main menu.");
        SceneManager.LoadScene("MainMenu"); //Changes the current scene to "MainMenu"
    }

    //Invocation: Called on click of 'Quit' button (in death menu)
    //Use: Exits the game
    public void gameQuit()
    {
        Debug.Log("Quit button pressed. Exiting game.");
        Application.Quit();
    }
}
