using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //This class will hold stats for the player, currently those are only movespeed and jumpheight.
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 jumpHeight; //jump height has to be done as a vector2 instead of a float, x is 0 and y gets changed.

    public bool levelOneAccessible = true;   //this is for the tutorial level
    public bool hubAccessible = false;      //will set this to true for finishing level 1
    public bool levelTwoAccessible = false;
    //add more as needed
    public float MoveSpeed { get => moveSpeed; }
    public Vector2 JumpHeight { get => jumpHeight; }

    public void setMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
}
