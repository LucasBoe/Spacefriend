using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] NavigationAgent agent;

    public void MoveTo(Vector3 point) => agent.MoveTo(point);
    public void MoveTo(Vector3 point, System.Action callback) => agent.MoveTo(point, callback);

}
