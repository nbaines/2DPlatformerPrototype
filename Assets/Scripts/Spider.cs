using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    public float speed = 3.0f;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;

    float timer;
    int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    //Changing directions
    void Update()
    {
        timer -= Time.deltaTime;

        //When timer reaches 0, spider changes direction, and timer is reset.
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    //Enemy movement
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        position.x = position.x + Time.deltaTime * speed * direction;

        rigidbody2D.MovePosition(position);
    }

    //Player damage on contact
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.modifyHealth(-10);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
