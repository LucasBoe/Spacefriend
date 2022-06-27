using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRoomNavigationGrid : MonoBehaviour
{
    [SerializeField] RoomBehaviour[] rooms;
    [SerializeField] NavigationGrid grid;

    private void OnEnable()
    {
        foreach (RoomBehaviour room in rooms)
            room.SetRoomStateEvent += OnSetRoomStateEvent;
    }

    private void OnDisable()
    {
        foreach (RoomBehaviour room in rooms)
            room.SetRoomStateEvent -= OnSetRoomStateEvent;
    }

    private void OnSetRoomStateEvent(bool roomActive)
    {
        if (roomActive) NavigationAgent.TriggerSwitchGridEvent(grid);
    }
}
