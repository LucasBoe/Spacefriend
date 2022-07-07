using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : SingletonBehaviour<RoomManager>
{
    public static Action<RoomData> TriggerEnterRoomEvent;
    public static Action<RoomInfo> OnChangeRoomEvent;

    [SerializeField, ReadOnly] private List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void OnEnable()
    {
        TriggerEnterRoomEvent += ChangeRoom;
    }
    private void OnDisable()
    {
        TriggerEnterRoomEvent -= ChangeRoom;
    }

    private void ChangeRoom(RoomData data)
    {
        foreach (RoomInfo roomInfo in roomInfos)
        {
            if (roomInfo.Data == data)
            {
                OnChangeRoomEvent?.Invoke(roomInfo);
            }
        }
    }

    public void RegisterRoom(RoomBehaviour roomBehaviour, RoomData roomData)
    {
        roomInfos.Add(new RoomInfo() { Data = roomData, SceneBehaviour = roomBehaviour });
    }

    public RoomBehaviour GetRoomInstanceByData(RoomData roomData)
    {
        return null;
    }
}

[System.Serializable]
public class RoomInfo
{
    public RoomData Data;
    public RoomBehaviour SceneBehaviour;
}
