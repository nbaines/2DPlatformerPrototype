using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>(); //access Player's health class
    }

    //Invocation: Called when another collider w/ a RigidBody2D enters base object's collider
    //Use: Damage the Player (only once)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Player stepped on spikes.");
            playerHealth.modifyHealth(-10);
        }
    }
}
