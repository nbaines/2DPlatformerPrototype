using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistFire : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy offscreen projectiles
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
            Debug.Log("Goodbye");
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    //Damage Player on contact, destory on contact with anything
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerHealth player = collider.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.modifyHealth(-10);
        }

        Debug.Log("Collision with " + collider.gameObject);
        Destroy(gameObject);
    }
}
