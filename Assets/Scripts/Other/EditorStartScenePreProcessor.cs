using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EditorStartScenePreProcessor
{
    public static int SceneStartedFromBuildIndex = 1;
    static EditorStartScenePreProcessor()
    {
        var scenePath = "Assets/Scenes/Main.unity";
        SceneStartedFromBuildIndex = EditorSceneManager.GetActiveScene().buildIndex;
        //EditorReferenceHolder.instance.PlayModeEditorSceneIndex = activeSceneIndex;
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
    }
}
#endif