using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    public float speed = 3.0f;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    private Animator animator; //used for sprite animations
    public PlayerController controller;

    float timer;
    int direction = -1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

        if (direction < 0) //control walk animation
        {
            animator.SetFloat("moveX", -1);
        }
        else if (direction > 0)
        {
            animator.SetFloat("moveX", 1);
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
        direction = 0;

        animator.SetBool("isDead", true);
        controller.PlayDeath("Spider");
        StartCoroutine(disableSpider());
        //Destroy(gameObject);
    }

    IEnumerator disableSpider()
    {
        yield return new WaitForSeconds(2.2f);
        Destroy(gameObject);
    }
}
