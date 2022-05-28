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
    [SerializeField, ReadOnly] public Vector2 DirectionalVector;

    float velocity = 0f;

    internal void MoveTo(Vector3 target, Action _callback = null)
    {
        currentPath = grid.GetPath(transform.position, target);
        callback = _callback;
    }

    internal void Move()
    {
        DirectionalVector = Vector2.zero;

        if (currentPath == null || currentPath.Count == 0)
        {
            velocity = 0f;
            return;
        }

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
            Vector3 dirRaw = (currentPath[0] - transform.position).normalized;
            DirectionalVector = new Vector2(Mathf.Round(dirRaw.x * 10f) / 10f, Mathf.Round(dirRaw.y * 10f) / 10f);

            if (distance > 1f)
                velocity = Mathf.MoveTowards(velocity, speed, Time.deltaTime * speed * 2);
            else
                velocity = Mathf.MoveTowards(velocity, 1, Time.deltaTime * speed * 2);

            transform.position = Vector3.MoveTowards(transform.position, currentPath[0], Time.deltaTime * velocity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + (Vector3)DirectionalVector);
    }
}
