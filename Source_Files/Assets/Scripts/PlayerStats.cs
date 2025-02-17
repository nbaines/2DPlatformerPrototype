﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //This class will hold stats for the player, currently those are only movespeed and jumpheight.
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 jumpHeight; //jump height has to be done as a vector2 instead of a float, x is 0 and y gets changed.
    [SerializeField]
    private Vector2 spawnPoint;
    //add more as needed
    public float MoveSpeed { get => moveSpeed; }
    public Vector2 JumpHeight { get => jumpHeight; }
    public Vector2 SpawnPoint { get => spawnPoint;}

    public void setMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
    public void SetSpawnPoint(Vector2 newSpawn)
    {
        spawnPoint = newSpawn;
    }
}
