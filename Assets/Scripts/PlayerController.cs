using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private new Rigidbody2D rigidbody; //these are assigned in inspector

    public LayerMask groundLayer;   //used in the isgrounded function
    public AudioSource audioS;      //plays the audio
    public AudioClip[] audioFiles;

    private string sceneName;
    private Animator animator; //used for sprite animations
    private bool currAttacking = false;

    void Start() {
        stats = GameObject.FindGameObjectWithTag("Persistance").GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        Movement();
        Jumper();
        Attack();

        //if (rigidbody.velocity.y < -1000)
        //{
        //    //velocity.y = -1000f;
        //}

        //Debug.Log("rigidbody.velocity.y: " + rigidbody.velocity.y);
        if (rigidbody.velocity.y < 0)
        {
            if (rigidbody.velocity.y > -0.0001)
            {
                animator.SetBool("isFalling", false);
            }
            else {
                animator.SetBool("isFalling", true);
            }

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
            if (!audioS.isPlaying && isGrounded(0.7f))
            {
                audioS.pitch = Random.Range(0.9f, 1.2f);
                Debug.Log(audioS.pitch);
                if ( sceneName == "Swamp Level")
                   audioS.PlayOneShot(audioFiles[1]);
                
                else if (sceneName == "Church Level")
                {
                    audioS.PlayOneShot(audioFiles[2]);//this will be implemented later
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
            if (audioS.isPlaying)
                audioS.Stop();
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
            if (audioS.isPlaying)
                audioS.Stop();
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
    bool isGrounded(float distance = 0.75f)
    {
        Vector2 currentPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.down, distance, groundLayer);
        if (hit.collider != null)
        {
            //animator.SetBool("isGrounded", true);
            return true;
        }
        else
        {
            //animator.SetBool("isGrounded", false);
            return false;
        }
    }


    void Attack()
    {
        if (!currAttacking && (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Return)))
        {
            audioS.PlayOneShot(audioFiles[0]);  //audioFiles[0] is the sword swing sfx
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
