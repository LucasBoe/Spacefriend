using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Hide all room navigation children game objects or make the whole structure mono behaviour indipendant
public class RoomNavigationGrid : NavigationGrid
{
    [SerializeField] Room room;
    private void OnValidate()
    {
        room = GetComponentInParent<Room>();
    }

    private void OnEnable()
    {
        room.SetRoomStateEvent += OnSetRoomStateEvent;
    }

    private void OnDisable()
    {
        room.SetRoomStateEvent -= OnSetRoomStateEvent;
    }

    private void OnSetRoomStateEvent(bool roomActive)
    {
        if (roomActive) NavigationAgent.TriggerSwitchEvent(this);
    }
}
