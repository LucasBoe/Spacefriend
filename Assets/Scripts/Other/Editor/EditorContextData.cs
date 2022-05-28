using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[CreateAssetMenu]
public class EditorContextData : ScriptableSingleton<EditorContextData>
{
    public AnimatorController PlayerAnimatorController;
}
