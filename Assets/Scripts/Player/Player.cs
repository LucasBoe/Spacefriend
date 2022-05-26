using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMoveModule MoveModule;
    public PlayerAnimationController AnimationController;
    public PlayerSkinModule SkinModule;

    private void Start()
    {
        SkinModule.SetSkinType(PlayerSkinType.Bed);
    }

    private void Update()
    {
        MoveModule.Update();
    }
}

[System.Serializable]
public class PlayerSkinModule
{
    [SerializeField] private PlayerSkinType skinType;
    public PlayerSkinType SkinType => skinType;
    public System.Action<PlayerSkinType> ChangedSkinTypeEvent;
    public void SetSkinType(PlayerSkinType newSkinType)
    {
        skinType = newSkinType;
        ChangedSkinTypeEvent?.Invoke(newSkinType);
    }
}

[System.Serializable]
public class PlayerMoveModule
{
    public PlayerMoveMode MoveMode;

    public void SetSpaceModeActive(bool spaceMode)
    {
        MoveMode = spaceMode ? PlayerMoveMode.SPACE : PlayerMoveMode.SPACE;
        spaceMover.SetSpaceMode(spaceMode);
    }

    private PlayerPositionOverrider positionOverride;

    [SerializeField] PlayerSpaceMover spaceMover;
    [SerializeField] NavigationAgent agent;
    [SerializeField] Transform playerTransform;

    public void WalkTo(Vector3 point, System.Action callback = null)
    {
        if (MoveMode == PlayerMoveMode.SPACE)
            spaceMover.MoveTo(point, callback);
        else
            agent.MoveTo(point, callback);
    }

    public void Update()
    {
        if (MoveMode == PlayerMoveMode.SPACE && PlayerManager.GetPlayerSkin() == PlayerSkinType.Space)
            spaceMover.Move();
        else if (MoveMode == PlayerMoveMode.FREE)
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
