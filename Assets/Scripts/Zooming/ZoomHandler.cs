using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ZoomHandler : SingletonBehaviour<ZoomHandler>
{
    ZoomState current = ZoomState.TOTAL;

    public static ZoomState CurrentZoom => Instance.current;

    public static System.Action<ZoomState,ZoomState> ChangedStateEvent;

    private bool isZooming = false;

    [ContextMenu("NextState")]
    public void TryZoom(int change)
    {
        ZoomState before = current;
        int number = (int)current;

        number += change;

        int max = System.Enum.GetValues(typeof(ZoomState)).Length - 1;

        number = Mathf.Clamp(number, 0, max);

        if (number != (int)current)
        {
            current = (ZoomState)number;
            ChangedStateEvent?.Invoke(before, current);
        }
    }

    public void TryZoomOut()
    {
        if (isZooming) return;

        TryZoom(-1);

        isZooming = true;
        CoroutineUtil.Delay(() => isZooming = false, this, 2f);
    }

    public void TryZoomIn()
    {
        if (isZooming) return;

        TryZoom(1);

        isZooming = true;
        CoroutineUtil.Delay(() => isZooming = false, this, 2f);
    }

}

[System.Serializable]
public enum ZoomState
{
    TOTAL,
    ROOM,
}
