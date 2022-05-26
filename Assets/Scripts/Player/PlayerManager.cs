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

    internal void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider)
    {
        player.MoveModule.SetPositionOverride(playerPositionOverrider);
    }

    internal void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider)
    {
        player.MoveModule.RevokeOverridePosition(playerPositionOverrider);
    }
}
