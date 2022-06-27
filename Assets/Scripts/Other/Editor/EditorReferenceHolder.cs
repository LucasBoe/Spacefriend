using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[FilePath("Other/EditorReferenceHolder.asset", FilePathAttribute.Location.PreferencesFolder)]
public class EditorReferenceHolder : ScriptableSingleton<EditorReferenceHolder>
{
    public AnimatorController PlayerAnimatorController;
}