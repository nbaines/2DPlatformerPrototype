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

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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

    //this function will break if there's no playerstats script attached to the same object as this
    //also the movement gets extremely confused when the triangle spins, but that shouldn't be a problem when we bring in a real sprite.
    //this function is called from update, and handles left right movement of the player using the horizontal inputs of the player.
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

    //this function is called from update and checks if the player ever presses spacebar. If they do, it then calls the
    //isGrounded function (see below for that function) to check if they're on the ground, if they are, it adds upwards force to the player
    //based on what the jumpHeight stats are set to in player stats, which are edited in the inspector.
    //for the future we can add a count to this script for number of jumps and allow 2 before calling isgrounded, things like that.
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

    //took this chunk of code on checking for if grounded from https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
    //how it works is this, we set a layer mask which specifies what layer we want to interact with, in this case it is the ground layer which
    //contains all the ground tiles - we'll have to remember to set all ground tiles to the ground layer.
    //once that's set, this will only get called if the player presses space, changed it so that we don't call this every frame.
    //this function shoots a ray downwards from the player's position, and checks within 1 unity unit for any ground tiles. 
    //if there are ground tiles within 1 unit, the hit will not be null and we're grounded, otherwise we're in the air.
    //can make some changes to allow double jumps and things like that, but for now this'll stop players from rocketing into the air
    //by mashing space. 
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
