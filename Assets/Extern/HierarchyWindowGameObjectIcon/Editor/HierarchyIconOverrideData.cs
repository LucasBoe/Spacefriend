using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HierarchyIconOverrideData : ScriptableSingleton<HierarchyIconOverrideData>
{
    [SerializeField] public StringIconPair[] Pairs = new StringIconPair[0];
}

[System.Serializable]
public class StringIconPair
{
    public string String;
    public Texture Icon;
}
