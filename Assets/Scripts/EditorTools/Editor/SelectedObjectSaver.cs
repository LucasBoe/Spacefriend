using UnityEditor;
using UnityEngine;

namespace Tools
{
    [InitializeOnLoad]
    public static class SelectedObjectSaver
    {
        static SelectedObjectSaver()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        static void OnPlayModeChanged(PlayModeStateChange obj)
        {
            Object selected = Selection.activeObject;
            if (selected != null)
                EditorPersistentDataStorage.LastSelectedObjectName = selected.name;
        }
    }
}