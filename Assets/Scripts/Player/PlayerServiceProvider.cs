using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sprouts.Physics.Player;

public class PlayerServiceProvider : SingletonBehaviour<PlayerServiceProvider>
{
    Player player;
    Animator playerAimator;
    ItemInHandController itemInHandController;
    RoomAgent roomAgent;
    SpaceAgent spaceAgent;

    PlayerPhysicsModule playerPhysics;
    public static PlayerInfoServive Info;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>(true);
        playerAimator = player.GetComponentInChildren<Animator>();
        itemInHandController = player.GetComponentInChildren<ItemInHandController>();
        roomAgent = player.GetComponent<RoomAgent>();
        spaceAgent = player.GetComponent<SpaceAgent>();
        playerPhysics = player.PhysicsModule;

        Info = new PlayerInfoServive();
    }
    public static void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider) => Instance.player.PhysicsModule.SetPositionOverride(playerPositionOverrider);
    public static void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider) => Instance.player.PhysicsModule.RevokeOverridePosition(playerPositionOverrider);
    public static Animator GetPlayerAnimator() =>  Instance.playerAimator;
    public static ItemData GetPlayerItemInHand() => Instance.itemInHandController.Item;
    public static void TeleportTo(Vector3 position) => Instance.player.PhysicsModule.TeleportTo(position);
    public static PlayerSkinType GetPlayerSkin() => Instance.player.SkinModule.SkinType;
    public static void SetPlayerSkin(PlayerSkinType skin) => Instance.player.SkinModule.SetSkinType(skin);
    public static void CollectItemToHand(ItemData data, Transform origin) => Instance.itemInHandController.SetItemInHand(data, origin);
    public static float RemoveItemFromHand(Transform target) => Instance.itemInHandController.SetItemInHand(null, target);
    public static RoomAgent GetRoomAgent() => Instance.roomAgent;
    public static SpaceAgent GetSpaceAgent() => Instance.spaceAgent;
    public static PlayerPhysicsModule GetPlayerPhysicsModule() => Instance.playerPhysics;
    public static Transform GetPlayerTransform() => Instance.player.transform;
    public static PlayerStates GetPlayerStates() => Instance.player.PhysicsModule.States;
}

public class PlayerInfoServive
{
    internal bool IsMoving()
    {
        return PlayerServiceProvider.GetPlayerPhysicsModule().GetDirectionalMoveVector().magnitude != 0;
    }
}
