using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ToggleCloseUp_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] CloseUp closeUp;

    public void Interact()
    {
        closeUp.Toggle();
    }

#if UNITY_EDITOR
    public string GetComponentName() => "ToggleCloseUp";
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty closeUpProperty = serializedObject.FindProperty("closeUp");
        EditorGUILayout.PropertyField(closeUpProperty);
    }
#endif
}
