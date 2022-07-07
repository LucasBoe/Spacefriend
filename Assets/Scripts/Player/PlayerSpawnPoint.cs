using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] int priority;
    [SerializeField] RoomInfo room;

    private static List<PlayerSpawnPointData> points = new List<PlayerSpawnPointData>();

    private void OnEnable()
    {
        points.Add(new PlayerSpawnPointData() { Point = this, Prio = priority });
    }

    public static PlayerSpawnPoint GetPlayerSpawnPoint()
    {
        return points.OrderBy(p => p.Prio).LastOrDefault().Point;
    }

    public Vector3 GetPoint()
    {
        Debug.Log("Teleport Player to " + name);
        return transform.position;
    }

    internal RoomInfo GetRoom()
    {
        return room;
    }
}

public class PlayerSpawnPointData
{
    public PlayerSpawnPoint Point;
    public int Prio;
}
