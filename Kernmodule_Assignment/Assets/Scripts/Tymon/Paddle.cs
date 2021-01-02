using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle
{
    /// <summary>
    /// The speed the player can move his bar with (bar meaning the block on the right side)
    /// </summary>
    private float _speed;
    /// <summary>
    /// The x-axis postion where the player object is locked in (meaning it cannot change the x-axis position from this)
    /// </summary>
    private float _xPositionLock;
    /// <summary>
    /// The transform that belongs to the paddle
    /// </summary>
    private Transform _paddleTransform;
    /// <summary>
    /// Controls this paddle
    /// </summary>
    private PaddleController _paddleController;

    /// <summary>
    /// Contstructor
    /// </summary>
    /// <param name="player">Reference towords the transform of the player</param>
    /// <param name="playerSpeed">The speed at wich the player can move</param>
    /// <param name="xPositionLock">The x-axis the player is locked on</param>
    public Paddle(float speed, float xPositionLock, Transform paddleTransform, PaddleController paddleController)
    {
        _speed = speed;
        _xPositionLock = xPositionLock;
        _paddleTransform = paddleTransform;
        _paddleController = paddleController;
    }

    /// <summary>
    /// Called by Tymon_Main to update this class
    /// </summary>
    public void Update()
    {
        // Get input from the paddle controller
        _paddleController.Update();
        // Move paddle
        _paddleTransform.position = Vector3.MoveTowards(_paddleTransform.position, new Vector3(_xPositionLock, _paddleTransform.position.y + _paddleController.InputDirection, 0), _speed * Time.deltaTime);
    }


}
