using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{

    public PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Persistance").GetComponent<PlayerStats>(); 
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("checkpoint reached!");
            stats.SetSpawnPoint(this.transform.position);
        }
    }
}
