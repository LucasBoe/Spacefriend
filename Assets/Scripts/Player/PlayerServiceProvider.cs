using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServiceProvider : SingletonBehaviour<PlayerServiceProvider>
{
    Player player;
    Animator playerAimator;
    ItemInHandController itemInHandController;
    RoomAgent roomAgent;
    SpaceAgent spaceAgent;
    PlayerMode playerMode;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
        playerAimator = player.GetComponentInChildren<Animator>();
        itemInHandController = player.GetComponentInChildren<ItemInHandController>();
        roomAgent = player.GetComponent<RoomAgent>();
        spaceAgent = player.GetComponent<SpaceAgent>();
        playerMode = player.Mode;
    }
    public static void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.SetPositionOverride(playerPositionOverrider);
    public static void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.RevokeOverridePosition(playerPositionOverrider);
    internal static PlayerMoveModule GetMoveModule() => Instance.player.MoveModule;
    public static Animator GetPlayerAnimator() =>  Instance.playerAimator;
    public static ItemData GetPlayerItemInHand() => Instance.itemInHandController.Item;
    public static PlayerSkinType GetPlayerSkin() => Instance.player.SkinModule.SkinType;
    public static void SetPlayerSkin(PlayerSkinType skin) => Instance.player.SkinModule.SetSkinType(skin);
    public static void CollectItemToHand(ItemData data, Transform origin) => Instance.itemInHandController.SetItemInHand(data, origin);
    public static void RemoveItemFromHand(Transform target) => Instance.itemInHandController.SetItemInHand(null, target);
    public static RoomAgent GetRoomAgent() => Instance.roomAgent;
    public static SpaceAgent GetSpaceAgent() => Instance.spaceAgent;
    public static PlayerMode GetPlayerMode() => Instance.playerMode;
    public static Transform GetPlayerTransform() => Instance.player.transform;
}
