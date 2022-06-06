using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool vertical;           //if true platform moves vertically, otherwise it moves horizontal
    public float speed = 3.0f;      //movement speed
    public float distance = 3.0f;   //distance before changing direction

    Rigidbody2D rigidbody2D;

    float timer;                    //Timer for directional change
    int direction = 1;              //Direction of the platform

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        //Initialize timer
        timer = distance;
    }

    // Update is called once per frame
    void Update()
    {
        //Decrement timer
        timer -= Time.deltaTime;

        //When timer runs out, reverse the direction and reset timer
        if (timer < 0)
        {
            direction = -direction;
            timer = distance;
        }
    }

    //Update function for platform movement
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical == true)
        {
            //vertical movement
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            //horizontal movement
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);
    }
}
