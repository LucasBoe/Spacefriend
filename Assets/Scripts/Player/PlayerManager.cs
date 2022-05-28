using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    Player player;
    Animator playerAimator;
    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
        playerAimator = player.GetComponentInChildren<Animator>();
    }

    public static void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.SetPositionOverride(playerPositionOverrider);

    public static Animator GetPlayerAnimator() =>  Instance.playerAimator;

    public static void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.RevokeOverridePosition(playerPositionOverrider);

    public static PlayerSkinType GetPlayerSkin() => Instance.player.SkinModule.SkinType;
    public static void SetPlayerSkin(PlayerSkinType skin) => Instance.player.SkinModule.SetSkinType(skin);
}
