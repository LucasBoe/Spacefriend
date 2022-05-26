using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private RoomSpriteRenderer[] roomSpriteRenderers;
    public System.Action<bool> SetRoomStateEvent;
    private void Awake()
    {
        roomSpriteRenderers = GetComponentsInChildren<RoomSpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomAgent roomAgent = collision.GetComponent<RoomAgent>();

        if (roomAgent != null)
            SetRoomState(roomAgent, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RoomAgent roomAgent = collision.GetComponent<RoomAgent>();

        if (roomAgent != null)
            SetRoomState(roomAgent, false);
    }

    private void SetRoomState(RoomAgent roomAgent, bool isInRoom)
    {
        SetRoomStateEvent?.Invoke(isInRoom);
        CoroutineUtil.ExecuteFloatRoutine(isInRoom ? 0f : 1f, isInRoom ? 1f : 0f, SetRoomSpriteRendererAlphas, this);
    }

    private void SetRoomSpriteRendererAlphas(float alpha)
    {
        foreach (RoomSpriteRenderer roomSpriteRenderer in roomSpriteRenderers)
        {
            roomSpriteRenderer.SetAlpha(alpha);
        }
    }
}
