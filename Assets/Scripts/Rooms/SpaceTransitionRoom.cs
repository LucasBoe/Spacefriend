using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTransitionRoom : Room
{
    [SerializeField] Collider2D roomCollider;

    List<SpaceRoomObject> spaceRoomObjects = new List<SpaceRoomObject>();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        SpaceRoomObject roomObject = collision.GetComponent<SpaceRoomObject>();
        if (roomObject != null) spaceRoomObjects.Add(roomObject);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        SpaceRoomObject roomObject = collision.GetComponent<SpaceRoomObject>();
        if (roomObject != null) spaceRoomObjects.Remove(roomObject);
    }

    public void SetRoomState(bool isSpace)
    {
        foreach (SpaceRoomObject spaceRoomObject in spaceRoomObjects)
        {
            spaceRoomObject.SetSpaceMode(isSpace);
        }
    }
}
