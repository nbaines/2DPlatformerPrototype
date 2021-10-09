using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuInterface;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Invert current pause state (paused/not paused)
        {
            if (isPaused)
                gameResume();
            else
                gamePause();
        }
    }

    //Invocation: Called on press of 'Esc' button, while game is paused
    //Use: Resumes the game
    public void gameResume()
    {
        pauseMenuInterface.SetActive(false); //disable pause menu & child elements
        Time.timeScale = 1f; //standard timescale
        isPaused = false;
    }

    //Invocation: Called on press of 'Esc' button, while game is NOT paused
    //Use: Pauses the game
    void gamePause()
    {
        pauseMenuInterface.SetActive(true); //enable pause menu & child elements
        Time.timeScale = 0f; //pause time
        isPaused = true;
    }

    //Invocation: Called on click of 'Main Menu' button (in pause menu)
    //Use: Returns player to the main menu
    public void gameReturnMainMenu()
    {
        Time.timeScale = 1f; //standard timescale

        Debug.Log("Main Menu button pressed. Returning player to main menu.");
        SceneManager.LoadScene("MainMenu"); //Changes the current scene to "MainMenu"
    }

    //Invocation: Called on click of 'Quit' button (in pause menu)
    //Use: Exits the game
    public void gameQuit()
    {
        Debug.Log("Quit button pressed. Exiting game.");
        Application.Quit();
    }
}
