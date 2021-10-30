using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    public float fireballFreq = 5.0f;           //How often cult man throws dat sick fire
    float timer;
    public int direction = -1;                  //Direction cultist is facing
    Vector2 lookDirection = new Vector2(1, 0);

    Rigidbody2D rigidbody2D;
    public GameObject fireballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = fireballFreq;

        if (direction > 0)
        {
            lookDirection.Set(1, 0);
        }
        else if (direction < 0)
        {
            lookDirection.Set(-1, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Debug.Log("Hadouken!");
            Fire();
            timer = fireballFreq;
        }
    }

    //Damage Player on Contact
    void OnCollisionEnter2D(Collision2D collider)
    {
        PlayerHealth player = collider.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.modifyHealth(-10);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    //Fireball attack
    void Fire()
    {
        CultistFire fireball;


        if (direction > 0)
        {
            GameObject fireballObject = Instantiate(fireballPrefab, rigidbody2D.position + Vector2.right * 0.5f + Vector2.down * 0.5f, Quaternion.identity);
            fireball = fireballObject.GetComponent<CultistFire>();
            fireball.Launch(lookDirection, 300);
        }
        else if (direction < 0)
        {
            GameObject fireballObject = Instantiate(fireballPrefab, rigidbody2D.position + Vector2.left * 0.5f + Vector2.down * 0.5f, Quaternion.identity);
            fireball = fireballObject.GetComponent<CultistFire>();
            fireball.Launch(lookDirection, 300);
        }
    }
}
