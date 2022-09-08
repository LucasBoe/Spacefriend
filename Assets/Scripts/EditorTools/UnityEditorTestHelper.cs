using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;

namespace Tools
{
    [ExecuteAlways]
    public class UnityEditorTestHelper : SingletonBehaviour<UnityEditorTestHelper>
    {
        [SerializeField] EditorPersistentDataStorage storage;
        void Start()
        {
            LoadEditorSelectionDelayed();
        }

        [Button]
        private void LoadEditorSelectionDelayed()
        {
            CoroutineUtil.Delay(() =>
            {
                string name = EditorPersistentDataStorage.LastSelectedObjectName;
                GameObject toSelect = GameObject.Find(name);
                Selection.activeObject = toSelect;
            }, this, 0.25f);
        }

        private void OnDestroy()
        {
            EditorPersistentDataStorage.EndSession();
        }
    }
}
#endif