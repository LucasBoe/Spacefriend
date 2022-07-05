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
        PlayerMoveModule moveModule = PlayerServiceProvider.GetMoveModule();

#if UNITY_EDITOR
        bool startFromStart = EditorPersistentDataStorage.TestFromStart;
        int index = EditorPersistentDataStorage.SceneStartedFromBuildIndex;
        if (!startFromStart && index != 0)
        {
            RoomBehaviour[] rooms = FindObjectsOfType<RoomBehaviour>();

            Debug.LogWarning("Found" + rooms.Length + " rooms.");

            foreach (RoomBehaviour room in rooms)
            {
                int roomDataIndex = room.Data.SceneIndex;
                if (roomDataIndex == index)
                {
                    currentRoom = new RoomInfo() { Data = room.Data, SceneBehaviour = room };
                    RoomManager.TriggerEnterRoomEvent(currentRoom.Data);

                    Debug.LogWarning(room);

                    PlayerSpawnPoint spawnPoint = room.SpawnPoint;


                    if (spawnPoint != null)
                    {
                        Debug.Log(spawnPoint.GetPoint());
                        moveModule.TeleportTo(spawnPoint.GetPoint());
                    }
                    else
                    {
                        Interactable closestDoor = GetClosestRoomChangeInteractable(transform.position);
                        if (closestDoor != null)
                            moveModule.TeleportTo(closestDoor.GetPoint());
                    }
                }
            }
        }
        else
        {
#endif
            PlayerSpawnPoint point = PlayerSpawnPoint.GetPlayerSpawnPoint();
            currentRoom = point.GetRoom();
            Debug.LogWarning("TriggerEnterRoomEvent");
            RoomManager.TriggerEnterRoomEvent(currentRoom.Data);
            moveModule.TeleportTo(point.GetPoint());
#if UNITY_EDITOR
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
