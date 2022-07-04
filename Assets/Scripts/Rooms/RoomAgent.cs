using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using Tools;
#endif

public class RoomAgent : MonoBehaviour
{
    [SerializeField] RoomInfo currentRoom;
    public RoomInfo CurrentRoom => currentRoom;

    private void Start()
    {
#if UNITY_EDITOR
        int index = EditorStartScenePreProcessor.SceneStartedFromBuildIndex;
        if (index != 0)
        {
            RoomBehaviour[] rooms = FindObjectsOfType<RoomBehaviour>();
            foreach (RoomBehaviour room in rooms)
            {
                Debug.Log(room);
                int roomDataIndex = room.Data.SceneIndex;
                if (roomDataIndex == index)
                {
                    currentRoom = new RoomInfo() { Data = room.Data, SceneBehaviour = room };
#endif
                    RoomManager.TriggerEnterRoomEvent(currentRoom.Data);
#if UNITY_EDITOR
                    Interactable closestDoor = GetClosestRoomChangeInteractable(transform.position);
                    if (closestDoor != null)
                        PlayerServiceProvider.GetMoveModule().TeleportTo(closestDoor.GetPoint());
                }
            }
        }
        else
        {
            RoomManager.TriggerEnterRoomEvent(currentRoom.Data);
        }
#endif
    }

    private void OnEnable()
    {
        RoomManager.OnChangeRoomEvent += OnChangeRoom;
    }
    private void OnDisable()
    {
        RoomManager.OnChangeRoomEvent -= OnChangeRoom;
    }

    private void OnChangeRoom(RoomInfo info)
    {
        currentRoom = info;
    }

    internal Interactable GetClosestRoomChangeInteractable(Vector3 cursorPoint)
    {
        Debug.LogWarning(currentRoom.SceneBehaviour);
        return currentRoom.SceneBehaviour.Doors.OrderBy(door => Vector2.Distance(door.transform.position, cursorPoint)).First().GetComponent<Interactable>();
    }
}
