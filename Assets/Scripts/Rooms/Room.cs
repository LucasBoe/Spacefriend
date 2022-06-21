using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public System.Action<bool> SetRoomStateEvent;
    public static System.Action<Room> TriggerEnterRoomEvent;

    private RoomSpriteRenderer[] roomSpriteRenderers;
    public bool IsActive;

    private void OnEnable()
    {
        TriggerEnterRoomEvent += OnEnterRoom;
    }
    private void OnDisable()
    {
        TriggerEnterRoomEvent -= OnEnterRoom;
    }

    private void OnEnterRoom(Room room)
    {
        SetRoomState(room == this);
    }

    private void Start()
    {
        roomSpriteRenderers = GetComponentsInChildren<RoomSpriteRenderer>();
        SetRoomSpriteRendererAlphas(0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        RoomAgent roomAgent = collision.GetComponent<RoomAgent>();

        if (roomAgent != null && !IsActive && PlayerServiceProvider.GetPlayerMode().IsOnShip)
            TriggerEnterRoomEvent(this);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        //RoomAgent roomAgent = collision.GetComponent<RoomAgent>();
        //
        //if (roomAgent != null)
        //    SetRoomState(false);
    }

    private void SetRoomState(bool isInRoom)
    {
        SetRoomStateEvent?.Invoke(isInRoom);
        IsActive = isInRoom;

        float duration = 0.5f;

        if (!isInRoom)
            CoroutineUtil.ExecuteFloatRoutine(1f, 0f, SetRoomSpriteRendererAlphas, this, duration);
        else
            CoroutineUtil.Delay(() => CoroutineUtil.ExecuteFloatRoutine(0f, 1f, SetRoomSpriteRendererAlphas, this, duration), this, 0);
    }

    private void SetRoomSpriteRendererAlphas(float alpha)
    {
        foreach (RoomSpriteRenderer roomSpriteRenderer in roomSpriteRenderers)
        {
            //TODO: make sure placing objects later on also adds them here...
            if (roomSpriteRenderer != null) roomSpriteRenderer.SetAlpha(alpha);
        }
    }
}
