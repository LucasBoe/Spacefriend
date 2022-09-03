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
            int index = EditorSceneManager.GetActiveScene().buildIndex;
            var mainPath = "Assets/Scenes/Main.unity";
            var ownPath = EditorSceneManager.GetActiveScene().path;
            EditorPersistentDataStorage.SceneStartedFromBuildIndex = index;
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(index < 0 ? ownPath: mainPath);
        }
    }
}
#endif