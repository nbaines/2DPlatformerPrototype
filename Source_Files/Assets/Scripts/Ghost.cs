using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool vertical;               //If true, Ghost moves vertically, otherwise Ghost moves horizontally
    public float speed = 3.0f;          //Speed of Gost's movement
    public float distance = 3.0f;       //Distance Ghost moves before changing directions
    public float dirChangeWaitTime = 1.0f;

    Rigidbody2D rigidbody2D;
    private Animator animator; //used for sprite animations
    private PlayerController controller;

    float timer;                //Timer for directional change
    public int direction = 1;          //Direction Ghost is moving

    bool changingDir = false;
    float baseSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        timer = distance;
    }

    //Changing Directions
    void Update()
    {
        //Decrement Timer
        timer -= Time.deltaTime;

        //Reset Timer and change direction when timer runs out
        if (timer < 0)
        {
            if (!changingDir)
            {
                changingDir = true;
                baseSpeed = speed;

                speed = 0.0f;

                Invoke("ChangeDir", dirChangeWaitTime);
            }

            //direction = -direction;
            //timer = distance;
        }

        if (direction < 0) //control walk animation
        {
            animator.SetFloat("moveX", -1);
        }
        else if (direction > 0)
        {
            animator.SetFloat("moveX", 1);
        }
    }

    //Update function for Ghost's movement
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            //move vertically
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            //move horizontally
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);

    }

    //Damage Player On Contact (Trigger cuz he's a spooky ghost who can go thru things)
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.modifyHealth(-10);
        }
    }

    public void Death()
    {
        direction = 0;

        animator.SetBool("isDead", true);
        controller.PlayDeath("Ghost");
        StartCoroutine(disableGhost());
        //Destroy(gameObject);
    }

    IEnumerator disableGhost()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }

    private void ChangeDir()
    {
        direction = -direction;
        timer = distance;

        changingDir = false;
        speed = baseSpeed;
    }
}
