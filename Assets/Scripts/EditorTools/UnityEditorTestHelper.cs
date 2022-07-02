using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;

namespace Tools
{
    [ExecuteInEditMode]
    public class UnityEditorTestHelper : MonoBehaviour
    {
        
        void Start()
        {
            LoadEditorSelectionDelayed();
        }

        [Button]
        private void LoadEditorSelectionDelayed()
        {
            CoroutineUtil.Delay(() =>
            {
                string name = EditorStartScenePreProcessor.GetSelected();
                GameObject toSelect = GameObject.Find(name);
                Selection.activeObject = toSelect;
            }, this, 0.5f);
        }
    }
}
#endif