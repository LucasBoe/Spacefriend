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
        public static int SceneStartedFromBuildIndex = 1;
        static string selectedName;
        static EditorStartScenePreProcessor()
        {
            var scenePath = "Assets/Scenes/Main.unity";
            SceneStartedFromBuildIndex = EditorSceneManager.GetActiveScene().buildIndex;
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            Debug.LogWarning("EditorStartScenePreProcessor");
        }

        public static void SetSelected(string name)
        {
            selectedName = name;
        }

        public static string GetSelected()
        {
            return selectedName;
        }
    }
}
#endif