using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkinType
{
    Bed = -2,
    Bath = -1,
    Default = 0,
    Space = 1
}

[System.Serializable]
public class PlayerSkin
{
    public PlayerSkinType Type;
    public Sprite PlaceholderSprite;
}
