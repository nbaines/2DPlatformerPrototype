using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        
        if (player != null)
        {
            if (player.currHealth < player.maxHealth)
            {
                player.modifyHealth(10);
                Destroy(gameObject);
            }
        }
    }
}
