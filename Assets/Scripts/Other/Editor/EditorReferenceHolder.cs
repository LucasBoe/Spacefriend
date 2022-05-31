using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[CreateAssetMenu]
public class EditorReferenceHolder : ScriptableSingleton<EditorReferenceHolder>
{
    public AnimatorController PlayerAnimatorController;
}
