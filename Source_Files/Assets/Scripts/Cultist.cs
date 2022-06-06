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
    private Animator animator; //used for sprite animations
    public GameObject fireballPrefab;
    public PlayerController controller;

    bool allowAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        timer = fireballFreq;

        if (direction > 0)
        {
            lookDirection.Set(1, 0);
            animator.SetFloat("direction", 1);
        }
        else if (direction < 0)
        {
            lookDirection.Set(-1, 0);
            animator.SetFloat("direction", -1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Debug.Log("Hadouken!");
            if (allowAttack)
            {
                Fire();
            }
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
        //direction = 0;
        allowAttack = false;

        animator.SetBool("isDead", true);
        StartCoroutine(disableCultist());
        controller.PlayDeath("Cultist");

        //Destroy(gameObject);
    }

    IEnumerator disableCultist()
    {
        yield return new WaitForSeconds(2.2f);
        Destroy(gameObject);
    }

    //Fireball attack
    void Fire()
    {
        animator.SetBool("isAttacking", true);
        StartCoroutine(ThrowFireball());
    }

    IEnumerator ThrowFireball()
    {
        yield return new WaitForSeconds(1.1f); //Wait for fireball throw animation (up to actual throw bit)

        CultistFire fireball;
        if (allowAttack)
        {
            if (direction > 0)
            {
                GameObject fireballObject = Instantiate(fireballPrefab, rigidbody2D.position + Vector2.right * 1.5f + Vector2.down * 0.5f, Quaternion.identity);
                fireball = fireballObject.GetComponent<CultistFire>();
                fireball.Launch(lookDirection, 300);
            }
            else if (direction < 0)
            {
                GameObject fireballObject = Instantiate(fireballPrefab, rigidbody2D.position + Vector2.left * 1.5f + Vector2.down * 0.5f, Quaternion.identity);
                fireball = fireballObject.GetComponent<CultistFire>();
                fireball.Launch(lookDirection, 300);
            }
        }

        // yield return new WaitForSeconds(0.3f); //Wait for throw's "settle" animation (remaining bits of atk anim.)
        yield return new WaitForSeconds(0.6f); //Wait for throw's "settle" animation (remaining bits of atk anim.)

        animator.SetBool("isAttacking", false);
    }
}
