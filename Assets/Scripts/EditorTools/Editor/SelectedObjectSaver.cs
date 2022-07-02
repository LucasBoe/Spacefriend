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
            EditorStartScenePreProcessor.SetSelected(EditorReferenceHolder.GetLastSelected());
        }


        static void OnPlayModeChanged(PlayModeStateChange obj)
        {
            Object selected = Selection.activeObject;
            if (selected != null)
                EditorReferenceHolder.StoreLastSelectedObject(selected);
        }
    }
}