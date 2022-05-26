using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMoveModule MoveModule;
    private void Update()
    {
        MoveModule.Update();
    }
}

[System.Serializable]
public class PlayerMoveModule
{
    public PlayerMoveMode MoveMode;
    private PlayerPositionOverrider positionOverride;

    [SerializeField] NavigationAgent agent;
    [SerializeField] Transform playerTransform;

    public void MoveTo(Vector3 point) => agent.MoveTo(point);


    public void MoveTo(Vector3 point, System.Action callback) => agent.MoveTo(point, callback);

    public void Update()
    {
        if (MoveMode == PlayerMoveMode.FREE)
            agent.Move();
        else
        {
            if (positionOverride != null)
                playerTransform.position = positionOverride.transform.position;
        }
    }
    public void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider)
    {
        Debug.Log("SetPositionOverride");

        positionOverride = playerPositionOverrider;
        MoveMode = PlayerMoveMode.OVERRIDEN;
    }
    public void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider)
    {
        if (playerPositionOverrider == positionOverride)
        {
            positionOverride = null;
            MoveMode = PlayerMoveMode.FREE;
        }
    }

    public enum PlayerMoveMode
    {
        FREE, //Tied to the nav grid, position is defined through the agent, interactions are possible
        OVERRIDEN, //Movement and animation can be overwritten by an interaction, interactions are not possible
        SPACE //Custom movement and animation, custom pyhsics and some interactions are blocked
    }
}
