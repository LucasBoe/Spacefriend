using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected virtual void OnSetRoomStateEvent(bool roomActive)
    {
        if (roomActive) NavigationAgent.TriggerSwitchEvent(this);
    }
}
