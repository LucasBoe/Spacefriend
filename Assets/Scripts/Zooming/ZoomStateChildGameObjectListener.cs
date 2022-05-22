using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomStateChildGameObjectListener : ZoomStateListenerBase
{
    protected override void OnChangedState(ZoomState newState)
    {
        bool active = newState == state;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
