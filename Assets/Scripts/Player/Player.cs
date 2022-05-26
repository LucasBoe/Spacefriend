using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] NavigationAgent agent;
    public void MoveTo(Vector3 point) => agent.MoveTo(point);
    public void MoveTo(Vector3 point, System.Action callback) => agent.MoveTo(point, callback);

    public enum PlayerMode
    {
        FREE, //Tied to the nav grid, position is defined through the agent, interactions are possible
        INTERACTION, //Movement and animation can be overwritten by an interaction, interactions are not possible
        SPACE //Custom movement and animation, custom pyhsics and some interactions are blocked
    }
}
