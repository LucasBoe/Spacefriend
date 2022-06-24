using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMinigame_InteractionListener : InteractionListenerBaseBehaviour
{
    [SerializeField] Minigame minigame;
    public override void Interact()
    {
        minigame.StartMinigame();
    }

#if UNITY_EDITOR
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty minigameProperty = serializedObject.FindProperty("minigame");
        EditorGUILayout.PropertyField(minigameProperty);
    }

#endif
}
