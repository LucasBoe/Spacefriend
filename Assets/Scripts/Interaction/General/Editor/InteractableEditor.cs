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
    bool editTargetOffset = false;
    bool showHoverListeners = true;
    bool showInteractionListeners = true;

    SerializedProperty hasTargetOffset;
    SerializedProperty walkTargetOffsetProperty;
    SerializedProperty hasCondititionsProperty;
    SerializedProperty conditionsProperty;

    Transform targetTransform;

    static bool originalComponentsVisible = false;

    private void OnEnable()
    {
        Interactable interactable = (Interactable)target;
        hoverListeners = interactable.GetComponents<IInteractableHoverListener>();
        interactionListeners = interactable.GetComponents<IInteractableInteractionListener>();
        SetComponentsVisible(originalComponentsVisible);

        hasTargetOffset = serializedObject.FindProperty("useWalkTargetOffset");
        hasCondititionsProperty = serializedObject.FindProperty("isConditioned");
        conditionsProperty = serializedObject.FindProperty("conditions");
        walkTargetOffsetProperty = serializedObject.FindProperty("walkTargetOffset");
        targetTransform = (target as Interactable).transform;
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


        if (GUILayout.Button("use custom walk target : " + hasTargetOffset.boolValue.ToString()))
        {
            hasTargetOffset.boolValue = !hasTargetOffset.boolValue;
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("use conditions : " + hasCondititionsProperty.boolValue.ToString()))
        {
            hasCondititionsProperty.boolValue = !hasCondititionsProperty.boolValue;
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

        if (hasTargetOffset.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = editTargetOffset;
            EditorGUILayout.PropertyField(walkTargetOffsetProperty);
            serializedObject.ApplyModifiedProperties();
            GUI.enabled = true;

            if (GUILayout.Button(new GUIContent(editTargetOffset ? "stop editing offset" : "edit offset", editIcon)))
                editTargetOffset = !editTargetOffset;

            EditorGUILayout.EndHorizontal();
        }

        if (hasCondititionsProperty.boolValue)
        {

            EditorGUILayout.PropertyField(conditionsProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OnSceneGUI()
    {
        if (editTargetOffset)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 globalAfter = Handles.DoPositionHandle(targetTransform.TransformPoint(walkTargetOffsetProperty.vector3Value), Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Walk Target Offset");
                Interactable interactable = target as Interactable;
                interactable.walkTargetOffset = targetTransform.InverseTransformPoint(globalAfter);
            }
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
