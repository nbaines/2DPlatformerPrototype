using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5.0f;

    [SerializeField] private Transform XTransform;
    [SerializeField] private Transform YTransform;
    [SerializeField] private Vector2 currentX;
    [SerializeField] private Vector2 currentY;
    [SerializeField] private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    { 
        XTransform = gameObject.GetComponent<Transform>();
        YTransform = gameObject.GetComponent<Transform>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentX = XTransform.position;
        currentY = YTransform.position;
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 mover = new Vector3 (horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        if (Input.GetKeyDown("space"))  //extremely rough outline of the beginning of the movement script. Needs to have changes made to stop jumping in the air.
        {                               //this also currently shoots the player up instead of moving them in a smooth arc, will need to work that out as well.
            mover.y += 0.5f;            //will keep tweaking it over the weekend and researching movement stuff.
        }                               //also also, if a player jumps they can effectively stick to walls.
        rigidbody.MovePosition(rigidbody.transform.position + mover);

    }
}
