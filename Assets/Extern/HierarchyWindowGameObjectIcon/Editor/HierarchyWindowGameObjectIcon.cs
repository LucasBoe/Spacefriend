using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchy window game object icon.
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-gameobject-icon/
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGameObjectIcon
{
    const string IgnoreIcons = "GameObject Icon, Prefab Icon, d_GameObject Icon, d_Prefab Icon";

    static HierarchyWindowGameObjectIcon()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);

        if (content.image != null && !IgnoreIcons.Contains(content.image.name))
            GUI.DrawTexture(new Rect(32, selectionRect.yMin, 16, 16), content.image);

    }
}