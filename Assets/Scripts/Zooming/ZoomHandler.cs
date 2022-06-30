using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using NaughtyAttributes;

public class ZoomHandler : SingletonBehaviour<ZoomHandler>
{
    [SerializeField, ReadOnly] ZoomState current = ZoomState.TOTAL;

    public static ZoomState CurrentZoom => Instance.current;

    public static System.Action<ZoomState, ZoomState> ChangedStateEvent;

    private bool isZooming = false;

    private void OnEnable()
    {
        GameModeManager.GameModeChangedEvent += OnGameModeChanged;
    }
    private void OnDisable()
    {
        GameModeManager.GameModeChangedEvent -= OnGameModeChanged;
    }


    private void OnGameModeChanged(GameMode from, GameMode to)
    {
        ZoomState before = current;
        current = to == GameMode.Total ? ZoomState.TOTAL : ZoomState.ROOM;
        if (before != current) ChangedStateEvent?.Invoke(before, current);
    }

    [ContextMenu("NextState")]
    public void TryZoom(int change)
    {
        int number = (int)current;

        number += change;

        int max = System.Enum.GetValues(typeof(ZoomState)).Length - 1;

        number = Mathf.Clamp(number, 0, max);

        if (number != (int)current)
        {
            ZoomState newState = (ZoomState)number;

            if (newState == ZoomState.TOTAL) GameModeManager.SetGameMode(GameMode.Total);
            else if (newState == ZoomState.ROOM) GameModeManager.SetGameMode(GameMode.Play);
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
