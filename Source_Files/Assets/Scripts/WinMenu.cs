using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenuInterface;
    bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Invert current pause state (paused/not paused)
        {
            if (hasWon) {
                Debug.Log("Quit button pressed. Exiting game.");
                Application.Quit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            winMenuInterface.SetActive(true);
            Time.timeScale = 0f; //pause time
            hasWon = true;
        }
    }


}
