using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMode Mode;
    public PlayerMoveModule MoveModule;
    public PlayerAnimationController AnimationController;
    public PlayerSkinModule SkinModule;

    private void OnEnable()
    {
        Mode.OnEnable();
    }

    private void OnDisable()
    {
        
       Mode.OnDisable();
    }

    private void Start()
    {
        SkinModule.SetSkinType(PlayerSkinType.Sleepy);
    }

    private void Update()
    {
        MoveModule.Update();
    }
}


[System.Serializable]
public class PlayerMode
{
    [SerializeField] SpaceAgent spaceAgent;

    [SerializeField, ReadOnly] PlayerModeType mode;
    public bool IsOnShip => mode == PlayerModeType.SHIP;
    public bool IsInSpace => mode == PlayerModeType.SPACE;

    public void SetMode(PlayerModeType newMode)
    {
        mode = newMode;
    }

    public void OnEnable() => spaceAgent.ChangeSpaceModeEvent += SetSpaceModeActive;
    public void OnDisable() => spaceAgent.ChangeSpaceModeEvent -= SetSpaceModeActive;
    private void SetSpaceModeActive(bool spaceMode) => SetMode(spaceMode ? PlayerModeType.SPACE : PlayerModeType.SHIP);
}

public enum PlayerModeType
{
    SHIP,
    SPACE,
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
    private bool overridePosition = false;
    private PlayerPositionOverrider positionOverride;
    private Vector2 directionalVectorFromOverride;
    [SerializeField] Transform playerTransform;

    [SerializeField] SpaceAgent spaceAgent;
    [SerializeField] NavigationAgent navigationAgent; 
    internal void MoveTo(Vector3 point, bool pointerOutsideOfCurrentRoom, System.Action callback = null)
    {
        if (PlayerServiceProvider.GetPlayerMode().IsInSpace)
        {
            spaceAgent.MoveTo(point, callback);
        }
        else
        {
            if (pointerOutsideOfCurrentRoom)
            {
                navigationAgent.MoveTo(point, () => PlayerServiceProvider.GetRoomAgent().GetClosestDoor(point).Interact());
            }
            else
            {
                navigationAgent.MoveTo(point, callback);
            }
        }
    }

    public void Update()
    {
        if (PlayerServiceProvider.GetPlayerMode().IsInSpace && PlayerServiceProvider.GetPlayerSkin() == PlayerSkinType.Astronaut)
            spaceAgent.Move();
        else if (!overridePosition)
            navigationAgent.Move();
        else
        {
            if (positionOverride != null)
            {
                Vector2 posBefore = playerTransform.position;
                Vector2 posAfter = positionOverride.transform.position;

                playerTransform.position = positionOverride.transform.position;

                if (Vector2.Distance(posBefore, posAfter) < Time.deltaTime)
                    directionalVectorFromOverride = Vector2.zero;
                else
                    directionalVectorFromOverride = (posAfter - posBefore).normalized;
            }
        }
    }
    public void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider)
    {
        positionOverride = playerPositionOverrider;
        overridePosition = true;
    }
    public void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider)
    {
        if (playerPositionOverrider == positionOverride)
        {
            positionOverride = null;
            overridePosition = false;
        }
    }
    public Vector3 GetDirectionalMoveVector() => overridePosition ? directionalVectorFromOverride : navigationAgent.DirectionalVector;
}
