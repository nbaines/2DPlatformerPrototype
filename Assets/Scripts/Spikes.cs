using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerStats playerStats;
    public GameObject player;
    public GameObject persistanceHolder;

    // Start is called before the first frame update

    private void Start()
    {
        persistanceHolder = GameObject.FindWithTag("Persistance");
        playerHealth = player.GetComponent<PlayerHealth>(); //access Player's health class
        playerStats = persistanceHolder.GetComponent<PlayerStats>();
    }


    //Invocation: Called when another collider w/ a RigidBody2D enters base object's collider
    //Use: Damage the Player (only once)
    private void OnTriggerStay2D(Collider2D collider)
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
