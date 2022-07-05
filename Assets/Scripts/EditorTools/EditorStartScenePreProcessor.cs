using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Tools
{
    [InitializeOnLoad]
    public static class EditorStartScenePreProcessor
    {
        static EditorStartScenePreProcessor()
        {
            var scenePath = "Assets/Scenes/Main.unity";
            EditorPersistentDataStorage.SceneStartedFromBuildIndex = EditorSceneManager.GetActiveScene().buildIndex;
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        }
    }
}
#endif