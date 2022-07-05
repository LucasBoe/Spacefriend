using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using System;

public static class EditorPersistentDataStoragePathInfo
{
    public const string PATH = "Assets/Other/DataStorage.asset";
}

[CreateAssetMenu]
[FilePath(EditorPersistentDataStoragePathInfo.PATH, FilePathAttribute.Location.ProjectFolder)]
public class EditorPersistentDataStorage : ScriptableSingleton<EditorPersistentDataStorage>
{
    [SerializeField, ReadOnly] string lastSelectedObjectName;
    [SerializeField, ReadOnly] int sceneStartedFromBuildIndex;
    [SerializeField, ReadOnly] bool testFromStart = false;

    public static string LastSelectedObjectName
    {
        set { instance.lastSelectedObjectName = value; }
        get { return instance.lastSelectedObjectName; }
    }
    public static int SceneStartedFromBuildIndex
    {
        set { instance.sceneStartedFromBuildIndex = value; }
        get { return instance.sceneStartedFromBuildIndex; }
    }

    public static bool TestFromStart
    {
        set { instance.testFromStart = value; }
        get { return instance.testFromStart; }
    }

    internal static void EndSession()
    {
        instance.testFromStart = false;
    }
}