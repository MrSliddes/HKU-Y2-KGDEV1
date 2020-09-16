﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tymon_Pongball
{
    public static Tymon_Pongball Instance { get; set; }

    /// <summary>
    /// The direction of the ball on the x-axis
    /// </summary>
    private float dirX = 1;
    /// <summary>
    /// The direction of the ball on the y-axis
    /// </summary>
    private float dirY = 1;
    /// <summary>
    /// The movement speed of the ball
    /// </summary>
    private float movementSpeed;
    /// <summary>
    /// Makes the ball go faster the more time gos on (resets when scored)
    /// </summary>
    private float movementSpeedScaler = 1;
    /// <summary>
    /// Reference to the pongball transform
    /// </summary>
    private Transform pongball = null;
    /// <summary>
    /// Reference to the player and enemy transform (I call them bars)
    /// </summary>
    private Transform[] bars;

    /// <summary>
    /// Set class values
    /// </summary>
    /// <param name="pongball">Reference to the pongball transform</param>
    /// <param name="movementSpeed">The movement speed of the ball</param>
    /// <param name="bars">Reference to the enemy and player transforms</param>
    public Tymon_Pongball(Transform pongball, float movementSpeed, Transform[] bars)
    {
        this.pongball = pongball;
        this.movementSpeed = movementSpeed;
        this.bars = bars;
        Instance = this;
    }

    /// <summary>
    /// Called by Tymon_Main to update this class
    /// </summary>
    public void Update()
    {
        Movement();
        Collision();
    }

    /// <summary>
    /// Move the ball in the right direction
    /// </summary>
    private void Movement()
    {
        // Move
        movementSpeedScaler += Time.deltaTime * 0.1f;
        pongball.position = new Vector3(pongball.position.x + dirX * movementSpeed * movementSpeedScaler * Time.deltaTime, pongball.position.y + dirY * movementSpeed * Time.deltaTime, 0);
        // Clamp inside camera view
        Vector3 pos = Camera.main.WorldToViewportPoint(pongball.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        // Change direction if pongball reached edge, add a point for the other side that scored
        if(pos.x == 0)
        {
            // Hit left wall, reset to middle, inverse dirX and add point for player
            pos.x = 0.5f;
            pos.y = 0.5f;
            dirX = 1;
            movementSpeedScaler = 1;
            movementSpeed = 5f;
            Tymon_Main.UpdateScore(new Vector2(0, 1));
        }
        else if(pos.x == 1)
        {
            // Hit right wall, reset to middle, inverse dirX and add point for enemy
            pos.x = 0.5f;
            pos.y = 0.5f;
            dirX = -1;
            movementSpeedScaler = 1;
            movementSpeed = 5f;
            Tymon_Main.UpdateScore(new Vector2(1, 0));
        }
        if(pos.y == 0) dirY = 1; else if(pos.y == 1) dirY = -1;
        // Set ponball position relative to camea viewport
        pongball.position = Camera.main.ViewportToWorldPoint(pos);
    }

    /// <summary>
    /// Check for a collision with the ball
    /// </summary>
    private void Collision()
    {
        foreach(Transform bar in bars)
        {
            // Check if ball pos hits barpos ( Bounce off),
            // Check x
            if(pongball.position.x <= bar.position.x + bar.localScale.x && pongball.position.x >= bar.position.x - bar.localScale.x)
            {
                // Check y
                if(pongball.position.y <= bar.position.y + bar.localScale.y && pongball.position.y >= bar.position.y - bar.localScale.y)
                {
                    // Bounche off in the right direction
                    if(pongball.position.x < bar.position.x) dirX = -1; else dirX = 1;
                    if(pongball.position.y < bar.position.y) dirY = -1; else dirY = 1;
                }
            }
        }
    }

    /// <summary>
    /// Change the current speed of the ball
    /// </summary>
    /// <param name="newSpeed">The new speed in float value</param>
    public static void ChangeSpeed(float newSpeed)
    {
        if(Instance == null)
        {
            Debug.LogError("Tymon_Ponball hasnt been created yet, create it first dummy");
            return;
        }
        Instance.movementSpeed += newSpeed;
        if(Instance.movementSpeed < 4) Instance.movementSpeed = 4;
    }
}
