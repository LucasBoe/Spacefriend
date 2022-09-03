using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil
{
    public static T AddChildWithComponent<T>(this GameObject gameObject, string name = "child") where T : Component
    {
        GameObject child = new GameObject(name); 
        child.transform.parent = gameObject.transform;
        return child.AddComponent<T>();
    }
}
