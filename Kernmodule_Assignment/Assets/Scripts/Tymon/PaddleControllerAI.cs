using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControllerAI : PaddleController
{
    /// <summary>
    /// Reference to the enemy transform
    /// </summary>
    private Transform _transform;
    /// <summary>
    /// Reference to the ball transform
    /// </summary>
    private Transform _ball;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enemy">Refrence to the enemy transform</param>
    /// <param name="ball">Reference to the ball transform</param>
    /// <param name="xPositionLock">The x-axis the enemy is locked on</param>
    /// <param name="enemySpeed">The speed at which the enemy can move</param>
    public PaddleControllerAI(Transform transform, Transform ball)
    {
        _transform = transform;
        _ball = ball;
    }

    public override void Update()
    {
        base.Update();

        // Change input
        if(_transform.position.y < _ball.position.y)
        {
            InputDirection = 1;
        }
        else if(_transform.position.y > _ball.position.y)
        {
            InputDirection = -1;
        }
        else
        {
            InputDirection = 0;
        }
    }
}
