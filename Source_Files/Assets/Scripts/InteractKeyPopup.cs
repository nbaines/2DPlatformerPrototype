using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractKeyPopup : MonoBehaviour
{
    public GameObject uiInteractKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            uiInteractKey.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            uiInteractKey.SetActive(true);
        }
    }
}
