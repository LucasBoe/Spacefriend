using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public System.Action<bool> SetRoomStateEvent;
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
    }
}
