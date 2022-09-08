using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Tools
{
    [InitializeOnLoad]
    public static class EditorStartScenePreProcessor
    {
        static EditorStartScenePreProcessor()
        {
            Scene ownScene = EditorSceneManager.GetActiveScene();
            SceneAsset loaderScene = EditorPersistentDataStorage.LoaderScene;
            EditorPersistentDataStorage.SceneStartedFromBuildIndex = ownScene.buildIndex;
            EditorSceneManager.playModeStartScene = ownScene.buildIndex < 0 ? AssetDatabase.LoadAssetAtPath<SceneAsset>(ownScene.path) : loaderScene;
        }
    }
}
#endif