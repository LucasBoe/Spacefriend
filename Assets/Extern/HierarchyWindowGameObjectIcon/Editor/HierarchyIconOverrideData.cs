using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class HierarchyIconOverrideData : ScriptableSingleton<HierarchyIconOverrideData>
{
    [SerializeField] public StringIconPair[] Pairs;
}

[System.Serializable]
public class StringIconPair
{
    public string String;
    public Texture Icon;
}
