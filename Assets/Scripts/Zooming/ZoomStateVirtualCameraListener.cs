using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class ZoomStateVirtualCameraListener : ZoomStateListenerBase
{
    CinemachineVirtualCamera virtualCamera;
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        ZoomHandler.ChangedStateEvent += OnChangedState;
    }
    private void OnDisable()
    {
        ZoomHandler.ChangedStateEvent -= OnChangedState;
    }

    protected override void OnChangedState(ZoomState previous, ZoomState newState)
    {
        virtualCamera.Priority = state == newState ? 1 : 0;
    }
}
