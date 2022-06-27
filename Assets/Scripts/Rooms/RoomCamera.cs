using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class RoomCamera : MonoBehaviour
{
    [SerializeField] RoomBehaviour room;
    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        room.SetRoomStateEvent += OnRoomStateChanged;
    }

    private void OnDisable()
    {
        room.SetRoomStateEvent -= OnRoomStateChanged;
    }

    private void OnRoomStateChanged(bool isInside)
    {
        virtualCamera.m_Priority = isInside ? 5 : 0;
    }
}
