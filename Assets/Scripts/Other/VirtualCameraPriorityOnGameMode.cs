using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCameraPriorityOnGameMode : MonoBehaviour
{
    [SerializeField] GameMode gameMode;
    [SerializeField] int truePrio, falsePrio;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void OnValidate()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        GameModeManager.GameModeChangedEvent += OnChangedGameMode;
    }
    private void OnDisable()
    {
        GameModeManager.GameModeChangedEvent -= OnChangedGameMode;
    }
    private void OnChangedGameMode(GameMode from, GameMode to)
    {
        var prio = (to == gameMode) ? truePrio : falsePrio;
        Debug.Log("set prio to: " + prio);
        virtualCamera.Priority = prio;
    }
}
