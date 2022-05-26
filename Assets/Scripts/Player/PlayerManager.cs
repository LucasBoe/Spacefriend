using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBehaviour<PlayerManager>
{
    Player player;
    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    public static void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.SetPositionOverride(playerPositionOverrider);
    public static void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider) => Instance.player.MoveModule.RevokeOverridePosition(playerPositionOverrider);

    public static PlayerSkinType GetPlayerSkin() => Instance.player.SkinModule.SkinType;
    public static void SetPlayerSkin(PlayerSkinType skin) => Instance.player.SkinModule.SetSkinType(skin);

    public static void SetSpaceMode(bool spaceMode) => Instance.player.MoveModule.SetSpaceModeActive(spaceMode);
}
