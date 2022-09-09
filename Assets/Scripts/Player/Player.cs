using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sprouts.Physics.Player;

public interface IPlayer
{
    PlayerStates GetPlayerStates();
    ItemData GetPlayerItemInHand();
    void TeleportTo(Vector3 position);
    PlayerSkinType GetSkin();
    bool IsMoving();
    void SetPlayerSkin(PlayerSkinType skin);
    RoomInfo GetCurrentRoom();
    float SetItemInHand(ItemData data, Transform origin);
    float RemoveItemFromHand(Transform transform);
    Transform GetPlayerTransform();
    Animator GetAnimator();
}

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerPhysicsModule PhysicsModule;
    [SerializeField] private PlayerAnimationController AnimationController;
    [SerializeField] private PlayerSkinModule SkinModule;
    [SerializeField] private ItemInHandController itemInHandController;
    [SerializeField] private RoomAgent roomAgent;

    public Action<PlayerSkinType> ChangedSkinTypeEvent { get; internal set; }

    public Transform GetPlayerTransform() => transform;
    public void TeleportTo(Vector3 position) => PhysicsModule.TeleportTo(position);
    public PlayerSkinType GetSkin() => SkinModule.SkinType;
    public void SetPlayerSkin(PlayerSkinType skin) => SkinModule.SetSkinType(skin);
    public ItemData GetPlayerItemInHand() => itemInHandController.Item;
    public float SetItemInHand(ItemData data, Transform origin) => itemInHandController.SetItemInHand(data, origin);
    public float RemoveItemFromHand(Transform transform) => itemInHandController.SetItemInHand(null, transform);
    public RoomInfo GetCurrentRoom() => roomAgent.CurrentRoom;
    public PlayerStates GetPlayerStates() => PhysicsModule.States;
    public bool IsMoving() => PhysicsModule.IsMoving;


    private void Start()
    {
        SkinModule.SetSkinType(PlayerSkinType.Sleepy);
    }

    public Animator GetAnimator()
    {
        return AnimationController.Animator;
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
