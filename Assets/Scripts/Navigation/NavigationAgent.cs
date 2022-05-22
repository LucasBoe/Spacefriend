using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAgent : MonoBehaviour
{
    [SerializeField] NavigationGrid grid;
    List<Vector3> currentPath = new List<Vector3>();
    internal void MoveTo(Vector3 target)
    {
        currentPath = grid.GetPath(transform.position, target);
    }

    private void Update()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        float distance = Vector2.Distance(transform.position, currentPath[0]);

        if (distance < 0.01f)
        {
            currentPath.RemoveAt(0);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPath[0], Time.deltaTime);
        }
    }
}
