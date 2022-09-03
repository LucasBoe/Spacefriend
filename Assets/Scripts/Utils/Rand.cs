using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rand
{
    public static float om(float max)
    {
        return om(0, max);
    }
    public static float om(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
