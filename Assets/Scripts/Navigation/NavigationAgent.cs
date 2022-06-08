using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAgent : MonoBehaviour
{
    [SerializeField] NavigationGrid grid;
    [SerializeField] float speedRegular = 3.5f, speedDown = 5f;
    List<Vector3> currentPath = new List<Vector3>();
    Action callback;
    [SerializeField, ReadOnly] public Vector2 DirectionalVector;

    float velocity = 0f;
    bool slidingDownBefore = false;
    public System.Action<bool> ChangeIsSlidingEvent;

    internal void MoveTo(Vector3 target, Action _callback = null)
    {
        currentPath = grid.GetPath(transform.position, target);
        callback = _callback;
    }

    internal void Move()
    {
        bool slidingDown = false;
        DirectionalVector = Vector2.zero;

        if (currentPath == null || currentPath.Count == 0)
        {
            velocity = 0f;
        }
        else
        {

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
                Vector3 targetPos = currentPath[0];

                Vector3 dirRaw = (targetPos - transform.position).normalized;
                DirectionalVector = new Vector2(Mathf.Round(dirRaw.x * 10f) / 10f, Mathf.Round(dirRaw.y * 10f) / 10f);

                slidingDown = (Mathf.Abs(DirectionalVector.x) < 0.5f && DirectionalVector.y < -0.5f);

                float speed = slidingDown ? speedDown : speedRegular;

                if (distance > 1f)
                    velocity = Mathf.MoveTowards(velocity, speed, Time.deltaTime * speed * 2);
                else
                    velocity = Mathf.MoveTowards(velocity, 1, Time.deltaTime * speed * 2);

                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * velocity);
            }
        }


        if (slidingDown != slidingDownBefore)
            ChangeIsSlidingEvent?.Invoke(slidingDown);

        slidingDownBefore = slidingDown;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + (Vector3)DirectionalVector);
    }
}
