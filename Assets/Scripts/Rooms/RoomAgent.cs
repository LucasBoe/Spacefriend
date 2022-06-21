using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomAgent : MonoBehaviour
{
    [SerializeField] Room currentRoom;
    public Room CurrentRoom => currentRoom;

    private void Start()
    {
        Room.TriggerEnterRoomEvent(currentRoom);
    }

    private void OnEnable()
    {
        Room.TriggerEnterRoomEvent += OnEnterRoom;
    }
    private void OnDisable()
    {
        Room.TriggerEnterRoomEvent -= OnEnterRoom;
    }

    private void OnEnterRoom(Room room)
    {
        currentRoom = room;
    }

    internal Interactable GetClosestRoomChangeInteractable(Vector3 cursorPoint)
    {
        ChangeRoom_InteractionListener[] doors = currentRoom.GetComponentsInChildren<ChangeRoom_InteractionListener>();
        return doors.OrderBy(door => Vector2.Distance(door.transform.position, cursorPoint)).First().GetComponent<Interactable>();
    }
}
