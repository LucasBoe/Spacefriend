using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : SingletonBehaviour<InputHandler>
{
    public static Action<Vector2> AxisInputEvent;

    private void FixedUpdate()
    {
        AxisInputEvent?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
