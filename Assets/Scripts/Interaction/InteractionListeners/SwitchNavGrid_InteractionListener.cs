using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SwitchNavGrid_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] NavigationGrid navGrid;
    public void Interact()
    {
        NavigationAgent.TriggerSwitchEvent?.Invoke(navGrid);
    }

#if UNITY_EDITOR
    public string GetComponentName() => "Switch Nav Grid";
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty navGridProperty = serializedObject.FindProperty("navGrid");
        EditorGUILayout.PropertyField(navGridProperty);
    }
#endif
}
