using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] NavigationAgent agent;

    internal void MoveTo(Vector3 point) => agent.MoveTo(point);

}
