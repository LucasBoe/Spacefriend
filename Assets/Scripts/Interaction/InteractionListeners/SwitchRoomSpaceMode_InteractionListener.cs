using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SwitchRoomSpaceMode_InteractionListener : InteractionListenerBaseBehaviour
{
    //[SerializeField] SpaceTransitionRoom transitionRoom;
    [SerializeField] Room spaceRoom;
    [SerializeField] bool space = true;
    [SerializeField] bool addSpaceSuite = true;

    public override void Interact()
    {
        SpaceAgent spaceAgent = PlayerServiceProvider.GetSpaceAgent();
        spaceAgent.SetSpaceMode(space);
        //transitionRoom.SetRoomState(isSpace: space);
        Room.TriggerEnterRoomEvent?.Invoke(spaceRoom);
        if (addSpaceSuite) PlayerServiceProvider.SetPlayerSkin(space ? PlayerSkinType.Astronaut : PlayerSkinType.Clothes);
    }

#if UNITY_EDITOR
    public override string GetComponentName() => "Switch Room (Space Mode)";
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty spaceRoomProperty = serializedObject.FindProperty("spaceRoom");
        EditorGUILayout.PropertyField(spaceRoomProperty);
        SerializedProperty spaceProperty = serializedObject.FindProperty("space");
        EditorGUILayout.PropertyField(spaceProperty);
        SerializedProperty addSpaceSuiteProperty = serializedObject.FindProperty("addSpaceSuite");
        EditorGUILayout.PropertyField(addSpaceSuiteProperty);
    }
#endif
}
