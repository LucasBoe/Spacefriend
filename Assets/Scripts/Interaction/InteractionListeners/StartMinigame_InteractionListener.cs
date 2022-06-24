using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMinigame_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] Minigame minigame;
    public void Interact()
    {
        minigame.StartMinigame();
    }

#if UNITY_EDITOR
    public string GetComponentName() => "Start Minigame";
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty minigameProperty = serializedObject.FindProperty("minigame");
        EditorGUILayout.PropertyField(minigameProperty);
    }
#endif
}
