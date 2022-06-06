using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioSource audioS;
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        
        if (player != null)
        {
            if (player.currHealth < player.maxHealth)
            {
                Debug.Log("Playing heart sfx");
                other.GetComponent<PlayerController>().PlayHeartPickup();
                player.modifyHealth(10);
                Destroy(gameObject);
            }
        }
    }
}
