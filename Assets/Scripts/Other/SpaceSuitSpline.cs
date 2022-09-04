using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitSpline : MonoBehaviour
{
    [SerializeField] SplineCreator creator;
    RoomAgent playerRoomAgent;
    Transform playerTransform;
    private void Start()
    {
        playerRoomAgent = PlayerServiceProvider.GetRoomAgent();
        playerTransform = PlayerServiceProvider.GetPlayerTransform();
    }
    private void Update()
    {
        if (playerRoomAgent.CurrentRoom.Data.IsSpace)
            creator.DrawSpline(playerTransform.position + Vector3.up, transform.position);
        else
            creator.DrawSpline(transform.position, transform.position);
    }
}
