using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchy window game object icon.
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-gameobject-icon/
/// additions by LucasBoe
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGameObjectIcon
{
    static HierarchyIconOverrideData overrides = null;
    const string IgnoreIcons = "GameObject Icon, Prefab Icon, d_GameObject Icon, d_Prefab Icon";

    static HierarchyWindowGameObjectIcon()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj == null) return;

        var content = EditorGUIUtility.ObjectContent(obj, null);

        Texture icon = null;

        if (content.image != null && !IgnoreIcons.Contains(content.image.name))
            icon = content.image;
        else
        {
            if (overrides == null)
                overrides = GetHierarchyIconOverrideData();
            else if (overrides.Pairs != null)
            {
                foreach (StringIconPair pair in overrides.Pairs)
                {
                    if (obj.name.Contains(pair.String))
                        icon = pair.Icon;
                }
            }
        }

        if (icon != null)
            GUI.DrawTexture(new Rect(32, selectionRect.yMin, 16, 16), icon);
    }

    static HierarchyIconOverrideData GetHierarchyIconOverrideData()
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(HierarchyIconOverrideData).Name);
        return AssetDatabase.LoadAssetAtPath<HierarchyIconOverrideData>(AssetDatabase.GUIDToAssetPath(guids[0]));
    }
}