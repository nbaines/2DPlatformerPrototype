using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //this class is going to exist here to hold all the stats of the player
    //these variables are private but serialized so i'm just changing them in the editor for now
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 jumpHeight; //jump height has to be done as a vector2 instead of a float, x is 0 and y gets changed.

    public float MoveSpeed { get => moveSpeed; }
    public Vector2 JumpHeight { get => jumpHeight; }
}
