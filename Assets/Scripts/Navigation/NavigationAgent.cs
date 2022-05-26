using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAgent : MonoBehaviour
{
    [SerializeField] NavigationGrid grid;
    [SerializeField] float speed = 10f;
    List<Vector3> currentPath = new List<Vector3>();
    Action callback;
    [SerializeField, ReadOnly] public Vector3 DirectionalVector;

    internal void MoveTo(Vector3 target, Action _callback = null)
    {
        currentPath = grid.GetPath(transform.position, target);
        callback = _callback;
    }

    internal void Move()
    {
        DirectionalVector = Vector3.zero;
        if (currentPath == null || currentPath.Count == 0) return;

        float distance = Vector2.Distance(transform.position, currentPath[0]);

        if (distance < 0.01f)
        {
            currentPath.RemoveAt(0);

            if (currentPath.Count == 0)
            {
                callback?.Invoke();
            }
        }
        else
        {
            DirectionalVector = (currentPath[0] - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, currentPath[0], Time.deltaTime * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + DirectionalVector);
    }
}
