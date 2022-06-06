using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Breakable"))
        {
            collider.GetComponent<VaseManager>().Break();
        }

        if (collider.CompareTag("Enemy"))
        {
            //Debug.Log("Hit something w/ 'enemy' tag");

            if (collider.GetComponent<Spider>() != null)
            {
                collider.GetComponent<Spider>().Death();
            }
            else if (collider.GetComponent<Ghost>() != null)
            {
                collider.GetComponent<Ghost>().Death();
            }
            else if (collider.GetComponent<Cultist>() != null)
            {
                collider.GetComponent<Cultist>().Death();
            }
        }
    }
}
