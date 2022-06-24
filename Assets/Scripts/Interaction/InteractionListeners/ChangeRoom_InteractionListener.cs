using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeRoom_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] Room room;

    public void Interact()
    {
        Room.TriggerEnterRoomEvent(room);
    }

#if UNITY_EDITOR
    public string GetComponentName() => "Change Room";
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty roomProperty = serializedObject.FindProperty("room");
        EditorGUILayout.PropertyField(roomProperty);
    }

#endif
}
