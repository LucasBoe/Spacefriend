using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sprouts.Physics.Player;

public class ServiceProvider : SingletonBehaviour<ServiceProvider>
{
    IPlayer player;
    public static IPlayer Player => Instance.player;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>(true);

    }
}
