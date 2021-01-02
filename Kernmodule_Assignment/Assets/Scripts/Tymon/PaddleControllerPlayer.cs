using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControllerPlayer : PaddleController
{
    // Store keycode to controll paddle
    public KeyCode _inputUp;
    public KeyCode _inputDown;

    // Constructor
    public PaddleControllerPlayer(KeyCode inputUp, KeyCode inputDown)
    {
        _inputUp = inputUp;
        _inputDown = inputDown;
    }

    public override void Update()
    {
        base.Update();

        // Change input
        if(Input.GetKey(_inputUp))
        {
            InputDirection = 1;
        }
        else if(Input.GetKey(_inputDown))
        {
            InputDirection = -1;
        }
        else
        {
            InputDirection = 0;
        }
    }
}
