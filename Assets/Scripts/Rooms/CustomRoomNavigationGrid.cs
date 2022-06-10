using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRoomNavigationGrid : MonoBehaviour
{
    [SerializeField] Room[] rooms;
    [SerializeField] NavigationGrid grid;

    private void OnEnable()
    {
        foreach (Room room in rooms)
            room.SetRoomStateEvent += OnSetRoomStateEvent;
    }

    private void OnDisable()
    {
        foreach (Room room in rooms)
            room.SetRoomStateEvent -= OnSetRoomStateEvent;
    }

    private void OnSetRoomStateEvent(bool roomActive)
    {
        if (roomActive) NavigationAgent.TriggerSwitchEvent(grid);
    }
}
