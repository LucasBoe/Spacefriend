using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class HierarchyIconOverrideData : ScriptableObject
{
    [SerializeField] public StringIconPair[] Pairs;
    [SerializeField] public Texture folderIconEditorSprite;
}

[System.Serializable]
public class StringIconPair
{
    public string String;
    public Texture Icon;
}
