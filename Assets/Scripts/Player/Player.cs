using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sprouts.Physics.Player;

public class Player : MonoBehaviour
{
    public PlayerPhysicsModule PhysicsModule;
    public PlayerAnimationController AnimationController;
    public PlayerSkinModule SkinModule;

    private void Start()
    {
        SkinModule.SetSkinType(PlayerSkinType.Sleepy);
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
