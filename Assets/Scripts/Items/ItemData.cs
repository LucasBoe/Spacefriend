using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [BoxGroup("WhenInHand")] public Vector3 InHandOffset;
    [BoxGroup("WhenInHand")] public bool InHandFlip;
}
