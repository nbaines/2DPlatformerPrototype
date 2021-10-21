using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerStats playerStats;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>(); //access Player's health class
        playerStats = player.GetComponent<PlayerStats>();
    }

    //Invocation: Called when another collider w/ a RigidBody2D enters base object's collider
    //Use: Damage the Player (only once)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player stepped on spikes.");
            playerHealth.modifyHealth(-10);

            //Slow player
            ////playerStats.setMoveSpeed(3); //TODO: Currently is set to a constant; make a variable!
        }
    }

    ////private void OnTriggerLeave2D(Collider2D collider)
    ////{
    ////    if (collider.CompareTag("Player"))
    ////    {
    ////        playerStats.setMoveSpeed(5); //TODO: Currently is set to a constant; make a variable!
    ////    }
    ////}
}
