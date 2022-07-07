using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SwitchNavGrid_InteractionListener : InteractableInteraction_BaseBehaviour
{
    [SerializeField] NavigationGrid navGrid;
    public override void Interact()
    {
        NavigationAgent.TriggerSwitchGridEvent?.Invoke(navGrid);
    }

#if UNITY_EDITOR
    public override string GetComponentName() => "Switch Nav Grid";
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty navGridProperty = serializedObject.FindProperty("navGrid");
        EditorGUILayout.PropertyField(navGridProperty);
    }
#endif
}
