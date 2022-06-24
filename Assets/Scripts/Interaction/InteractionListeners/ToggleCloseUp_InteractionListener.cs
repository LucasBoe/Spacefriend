using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ToggleCloseUp_InteractionListener : InteractionListenerBaseBehaviour
{
    [SerializeField] CloseUp closeUp;

    public override void Interact()
    {
        closeUp.Toggle();
    }

#if UNITY_EDITOR
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty closeUpProperty = serializedObject.FindProperty("closeUp");
        EditorGUILayout.PropertyField(closeUpProperty);
    }
#endif
}
