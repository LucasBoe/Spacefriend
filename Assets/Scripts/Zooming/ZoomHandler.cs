using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomHandler : MonoBehaviour
{
    ZoomState current = ZoomState.TOTAL;

    public static System.Action<ZoomState> ChangedStateEvent;

    [ContextMenu("NextState")]
    public void NextState()
    {
        int number = (int)current;

        number++;

        if (number > System.Enum.GetValues(typeof(ZoomState)).Length - 1)
            number = 0;

        current = (ZoomState)number;

        ChangedStateEvent?.Invoke(current);
    }
}

[System.Serializable]
public enum ZoomState
{
    TOTAL,
    WHOLE_SHIP,
    ROOM1,
}
