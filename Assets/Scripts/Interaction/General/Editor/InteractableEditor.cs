using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Interactable))]
[CanEditMultipleObjects]
public class InteractableEditor : Editor
{
    IInteractableHoverListener[] hoverListeners;
    IInteractionListener[] interactListeners;

    bool editName = false;

    private void OnEnable()
    {
        Interactable interactable = (Interactable)target;
        hoverListeners = interactable.GetComponents<IInteractableHoverListener>();
        interactListeners = interactable.GetComponents<IInteractionListener>();
    }

    protected override void OnHeaderGUI()
    {
        EditorGUILayout.LabelField("Header!");
    }
    public override void OnInspectorGUI()
    {
        var rect = EditorGUILayout.GetControlRect(false, 0f);


        //serializedObject.Update();

        //DrawDefaultInspector();

        EditorGUILayout.BeginHorizontal();
        Texture2D editIcon = EditorGUIUtility.FindTexture("SceneViewTools@2x");
        float size = 0.8f * EditorGUIUtility.singleLineHeight;
        EditorGUIUtility.SetIconSize(new Vector2(size, size));

        GUIStyle iconButtonStyle = new GUIStyle(GUI.skin.button);
        int padding = Mathf.RoundToInt(size * 0.125f);
        iconButtonStyle.padding = new RectOffset(padding, padding, padding, padding);
        if (GUILayout.Button(new GUIContent("edit name", editIcon), iconButtonStyle))
        {
            editName = !editName;
        }
        SerializedProperty nameProperty = serializedObject.FindProperty("DisplayName");
        if (editName)
        {
            EditorGUILayout.PropertyField(nameProperty, new GUIContent(""));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            serializedObject.ApplyModifiedProperties();
        }

        rect.height = EditorGUIUtility.singleLineHeight;
        rect.y -= rect.height * 1.35f;
        rect.x = rect.width / 1.5f;

        EditorGUI.LabelField(rect, nameProperty.stringValue, EditorStyles.boldLabel);

        SerializedProperty customTargetProperty = serializedObject.FindProperty("useCustomWalkTargetPoint");
        if (GUILayout.Button("use custom walk target : " + customTargetProperty.boolValue.ToString()))
        {
            customTargetProperty.boolValue = !customTargetProperty.boolValue;
            serializedObject.ApplyModifiedProperties();
        }

        SerializedProperty isConditionedProperty = serializedObject.FindProperty("isConditioned");
        if (GUILayout.Button("use conditions : " + isConditionedProperty.boolValue.ToString()))
        {
            isConditionedProperty.boolValue = !isConditionedProperty.boolValue;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Label("HoverListeners", "PR Label");
        EditorGUILayout.BeginVertical("helpBox");

        int count = 0;

        foreach (IInteractableHoverListener item in hoverListeners)
        {
            count++;
            GUILayout.BeginHorizontal();
            GUILayout.Label(count + ") " + item.GetComponentName(), "BoldLabel");
            GUILayout.Button("x", GUILayout.Width(32));
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("helpBox");
            item.DrawInspector();
            GUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();

        GUILayout.Label("InteractionListeners", "PR Label");
        EditorGUILayout.BeginVertical("helpBox");

        count = 0;

        foreach (IInteractionListener item in interactListeners)
        {
            count++;
            GUILayout.BeginHorizontal();
            GUILayout.Label(count + ") " + item.GetComponentName(), "BoldLabel");
            GUILayout.Button("x", GUILayout.Width(32));
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("helpBox");
            item.DrawInspector();
            GUILayout.EndVertical();
        }

        EditorGUILayout.EndVertical();
    }
}
