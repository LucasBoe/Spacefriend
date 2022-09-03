using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineRendererUtil
{
    public static void UpdatePoints(this LineRenderer lineRenderer, List<Vector2> vector2s)
    {
        int c = vector2s.Count;
        lineRenderer.positionCount = c;
        for (int i = 0; i < c; i++)
        {
            lineRenderer.SetPosition(i, vector2s[i]);
        }
    } 
}
