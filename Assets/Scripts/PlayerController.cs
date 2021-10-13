using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private new Rigidbody2D rigidbody; //these are assigned in inspector

    public LayerMask groundLayer;   //used in the isgrounded function
    private Animator animator; //used for sprite animations
    private bool currAttacking = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        Jumper();
        Attack();
        if (Input.GetKeyDown(KeyCode.LeftAlt))  //DEBUG: lets you reset player position if you fall off of the map here.
        {
            this.transform.position = new Vector3(-2.0f, 1.0f, 0f);
        }

        if (rigidbody.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    //Pre: Called from update, requires the object it is attached to to have a playerstats script attached as well. This function will take
    //a player's horizontal movement inputs (vertical handled in the jumper function below) and convert them to movement.
    //Post: This function will move a player left or right based on if they press A or D on the keyboard, at a rate of speed based on the
    //MoveSpeed variable in PlayerStats.
    void Movement()
    {
        float xInput = Input.GetAxis("Horizontal"); //momentum move (gradual key press)
        ////float xInput = Input.GetAxisRaw("Horizontal"); //non-momentum move (binary key press)


        //Handle idle/move animations
        ////Debug.Log("input x: " + xInput.ToString());
        if (xInput != 0) //if not moving, keep previous animation loop
        {
            if (xInput < 0)
            {
                animator.SetFloat("moveX", -1);
            }
            else {
                animator.SetFloat("moveX", 1);
            }

            //animator.SetFloat("moveX", xInput);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        Vector2 speed = new Vector2(xInput, 0f) * stats.MoveSpeed * Time.deltaTime;

        this.transform.Translate(speed);
    }

    //Pre: This function is called from update, and listens for a player to press the spacebar. This function relies on another function
    //named isGrounded to check if the player is on the ground and is legal to jump.
    //this function also needs this script to be attached to an object that has player stats in it, so it can access player jump height.
    //Post: If the player presses spacebar on te ground, the player will have their y velocity increased 
    void Jumper()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rigidbody.AddForce(stats.JumpHeight, ForceMode2D.Impulse);
            }
        }

        if (rigidbody.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    //taken from: https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
    //Pre: This Function will be called when the player presses space. this function expects players to be standing on objects tagged as
    //ground objects, otherwise as far as we are concerned the player is flying. T
    //Post: When the player presses space, this function will shoot a ray from the center of the player downwards (negative y direction) a 
    //distance variable to check if the player is on the ground. If the ray hits something in that distance, this function returns true
    //otherwise it returns false.
    bool isGrounded()
    {
        Vector2 currentPos = this.transform.position;
        float distance = 0.75f;  //this value has to be tweaked so that players can't jump again while they're in the air but close to the ground.
                                //0.25f seems to be the sweet spot, but this may change when we put in a new sprite for the player.
        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.down, distance, groundLayer);
        if (hit.collider != null)
            return true;
        else
            return false;
    }


    void Attack()
    {
        if (!currAttacking && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Return)))
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        animator.SetBool("isAttacking", true);
        currAttacking = true;
        yield return null;

        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
        currAttacking = false;
    }
}
