using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistFire : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //Destroy offscreen projectiles
        if (timer < 0)
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
