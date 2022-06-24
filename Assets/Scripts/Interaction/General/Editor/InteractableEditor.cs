using System;
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
    bool showHoverListeners = true;
    bool showInteractListeners = true;

    static bool componentsVisible = false;

    private void OnEnable()
    {
        Interactable interactable = (Interactable)target;
        hoverListeners = interactable.GetComponents<IInteractableHoverListener>();
        interactListeners = interactable.GetComponents<IInteractionListener>();
        SetComponentsVisible(componentsVisible);
    }
    public override void OnInspectorGUI()
    {
        var rect = EditorGUILayout.GetControlRect(false, 0f);

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
        rect.x = rect.width / 2f;

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

        if (GUILayout.Button(EditorGUIUtility.FindTexture(componentsVisible ? "d_scenevis_visible_hover@2x" : "d_scenevis_hidden_hover@2x")))
        {
            SetComponentsVisible(!componentsVisible);
            EditorUtility.SetDirty((target as Interactable).gameObject);
        }

        EditorGUILayout.EndHorizontal();

        if (hoverListeners.Length > 0)
        {
            showHoverListeners = EditorGUILayout.Foldout(showHoverListeners, "Hover Listeners (" + hoverListeners.Length + ")");
            EditorGUILayout.BeginVertical("helpBox");
            DrawListenerGUIs(hoverListeners);
            EditorGUILayout.EndVertical();
        }


        if (interactListeners.Length > 0)
        {
            showInteractListeners = EditorGUILayout.Foldout(showInteractListeners, "Interaction Listeners (" + interactListeners.Length + ")");
            EditorGUILayout.BeginVertical("helpBox");
            DrawListenerGUIs(interactListeners);
            EditorGUILayout.EndVertical();
        }

        //TODO: draw nice visualization for the custom walk target, maybe make it into a vector three with handle, so you don't need a transform, or hide the transform?
        if (customTargetProperty.boolValue)
        {
            SerializedProperty targetProperty = serializedObject.FindProperty("customWalkTargetTransform");
            EditorGUILayout.PropertyField(targetProperty);
            serializedObject.ApplyModifiedProperties();
        }

        if (isConditionedProperty.boolValue)
        {
            SerializedProperty targetProperty = serializedObject.FindProperty("conditions");
            EditorGUILayout.PropertyField(targetProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void SetComponentsVisible(bool visible)
    {
        componentsVisible = visible;
        foreach (IInteractionListenerBase item in hoverListeners) item?.SetVisible(visible);
        foreach (IInteractionListenerBase item in interactListeners) item?.SetVisible(visible);
    }

    private static void DrawListenerGUIs(IInteractionListenerBase[] toDraw)
    {
        for (int i = 0; i < toDraw.Length; i++)
        {
            IInteractionListenerBase item = toDraw[i];
            GUILayout.BeginHorizontal();
            GUILayout.Label((i + 1) + ") " + item.GetComponentName(), "BoldLabel");

            GUIStyle iconButtonStyle = new GUIStyle(GUI.skin.button);
            int padding = 0;
            iconButtonStyle.padding = new RectOffset(padding, padding, padding, padding);

            if (GUILayout.Button("x", GUILayout.Width(EditorGUIUtility.singleLineHeight)))
                item.RemoveComponent();

            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("helpBox");
            item.DrawInspector();
            GUILayout.EndVertical();
        }
    }
}
