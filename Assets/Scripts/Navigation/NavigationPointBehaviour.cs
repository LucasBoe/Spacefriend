using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointBehaviour : MonoBehaviour, INavigationPoint
{
    public Vector3 Position => transform.position;
}
