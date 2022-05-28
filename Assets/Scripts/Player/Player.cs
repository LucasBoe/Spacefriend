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

    private void OnEnable()
    {
        MoveModule.OnEnable();
    }

    private void OnDisable()
    {
        MoveModule.OnDisable();
    }

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
    private PlayerPositionOverrider positionOverride;
    [SerializeField] Transform playerTransform;

    [SerializeField] SpaceAgent spaceAgent;
    [SerializeField] NavigationAgent navigationAgent;

    public void OnEnable() => spaceAgent.ChangeSpaceModeEvent += SetSpaceModeActive;
    public void OnDisable() => spaceAgent.ChangeSpaceModeEvent -= SetSpaceModeActive;
    private void SetSpaceModeActive(bool spaceMode) => MoveMode = spaceMode ? PlayerMoveMode.SPACE : PlayerMoveMode.SPACE;

    public void WalkTo(Vector3 point, System.Action callback = null)
    {
        if (MoveMode == PlayerMoveMode.SPACE)
            spaceAgent.MoveTo(point, callback);
        else
            navigationAgent.MoveTo(point, callback);
    }

    public void Update()
    {
        if (MoveMode == PlayerMoveMode.SPACE && PlayerManager.GetPlayerSkin() == PlayerSkinType.Space)
            spaceAgent.Move();
        else if (MoveMode == PlayerMoveMode.FREE)
            navigationAgent.Move();
        else
        {
            if (positionOverride != null)
                playerTransform.position = positionOverride.transform.position;
        }
    }
    public void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider)
    {
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

    public Vector3 GetDirectionalMoveVector() => navigationAgent.DirectionalVector;
    public enum PlayerMoveMode
    {
        FREE, //Tied to the nav grid, position is defined through the agent, interactions are possible
        OVERRIDEN, //Movement and animation can be overwritten by an interaction, interactions are not possible
        SPACE //Custom movement and animation, custom pyhsics and some interactions are blocked
    }
}
