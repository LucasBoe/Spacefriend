using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sprouts.Physics.Edit
{
    [CustomEditor(typeof(RoomPhysics))]
    public class RoomPhysicsEditor : Editor
    {
        bool deleteMode = false;
        RoomPhysics roomPhysics;
        private void OnEnable()
        {
            roomPhysics = target as RoomPhysics;
        }

        // Custom in-scene UI for when ExampleScript
        // component is selected.
        public void OnSceneGUI()
        {
            for (int i = 0; i < roomPhysics.RoomBoundaries.Count; i++)
            {
                Vector2 before = roomPhysics.RoomBoundaries[(i == 0 ? roomPhysics.RoomBoundaries.Count : i) - 1];
                Vector2 point = roomPhysics.RoomBoundaries[i];

                Handles.color = Color.cyan;
                Handles.DrawLine(before, point);

                Vector2 inbetween = Vector2.Lerp(before, point, 0.5f);

                if (Handles.Button(inbetween, Quaternion.identity, 0.1f, 0.2f, Handles.CircleHandleCap))
                {
                    roomPhysics.RoomBoundaries.Insert(i, inbetween);
                    break;
                }

                Event e = Event.current;

                if (e.isKey && e.keyCode == KeyCode.LeftControl)
                {
                    if (deleteMode && e.type == EventType.KeyUp)
                        deleteMode = false;
                    else if (!deleteMode && e.type == EventType.KeyDown)
                        deleteMode = true;

                    Debug.Log(deleteMode);
                }


                if (deleteMode)
                {

                    Handles.color = Color.red;
                    if (Handles.Button(point, Quaternion.identity, 0.3f, 0.35f, Handles.CircleHandleCap))
                        roomPhysics.RoomBoundaries.RemoveAt(i);
                }
                else
                {
                    Handles.color = Color.green;
                    EditorGUI.BeginChangeCheck();
                    Vector2 newPoint = Handles.FreeMoveHandle(point, Quaternion.identity, 0.25f, Vector3.one, Handles.CubeHandleCap);
                    if (EditorGUI.EndChangeCheck())
                    {
                        roomPhysics.RoomBoundaries[i] = newPoint;
                        Undo.RecordObject(roomPhysics, "Change Look At Target Position");
                    }
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Update Gravity"))
            {
                roomPhysics.UpdateGravity();
            }
        }
    }
}