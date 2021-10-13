using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Breakable"))
        {
            collider.GetComponent<VaseManager>().Break();
        }

        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hello");
            collider.GetComponent<Spider>().Death();
        }
    }
}
