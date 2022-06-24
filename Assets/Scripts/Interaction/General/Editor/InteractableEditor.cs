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
    IInteractableInteractionListener[] interactionListeners;

    bool editName = false;
    bool showHoverListeners = true;
    bool showInteractionListeners = true;

    static bool originalComponentsVisible = false;

    private void OnEnable()
    {
        Interactable interactable = (Interactable)target;
        hoverListeners = interactable.GetComponents<IInteractableHoverListener>();
        interactionListeners = interactable.GetComponents<IInteractableInteractionListener>();
        SetComponentsVisible(originalComponentsVisible);
    }
    public override void OnInspectorGUI()
    {
        var rect = EditorGUILayout.GetControlRect(false, 0f);
        float size = 0.8f * EditorGUIUtility.singleLineHeight;
        EditorGUIUtility.SetIconSize(new Vector2(size, size));

        EditorGUILayout.BeginHorizontal();
        Texture2D editIcon = EditorGUIUtility.FindTexture("SceneViewTools@2x");

        if (GUILayout.Button(new GUIContent("edit name", editIcon)))
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

        if (GUILayout.Button(EditorGUIUtility.FindTexture(originalComponentsVisible ? "d_scenevis_visible_hover@2x" : "d_scenevis_hidden_hover@2x")))
        {
            SetComponentsVisible(!originalComponentsVisible);
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


        if (interactionListeners.Length > 0)
        {
            showInteractionListeners = EditorGUILayout.Foldout(showInteractionListeners, "Interaction Listeners (" + interactionListeners.Length + ")");
            EditorGUILayout.BeginVertical("helpBox");
            DrawListenerGUIs(interactionListeners);
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
        originalComponentsVisible = visible;
        foreach (IInteractableListenerBase item in hoverListeners) item?.SetVisible(visible);
        foreach (IInteractableListenerBase item in interactionListeners) item?.SetVisible(visible);
    }

    private static void DrawListenerGUIs(IInteractableListenerBase[] toDraw)
    {
        for (int i = 0; i < toDraw.Length; i++)
        {
            IInteractableListenerBase item = toDraw[i];
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
