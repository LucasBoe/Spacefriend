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
    const string IgnoreIcons = "GameObject Icon, Prefab Icon, d_GameObject Icon, d_Prefab Icon, d_PrefabVariant Icon";

    static HierarchyWindowGameObjectIcon()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj == null) return;

        var content = EditorGUIUtility.ObjectContent(obj, null);

        if (overrides == null)
            overrides = GetHierarchyIconOverrideData();

        Texture icon = null;

        if (content.image != null && !IgnoreIcons.Contains(content.image.name))
        {

            icon = content.image;
        }
        else
        {
            if (obj.name.Contains("=="))
            {
                Texture2D overlayTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                overlayTex.SetPixel(0, 0, new Color(0, 0, 1, 0.1f));
                overlayTex.Apply();
                GUI.DrawTexture(selectionRect, overlayTex, ScaleMode.StretchToFill);

                if (overrides != null)
                    GUI.DrawTexture(new Rect(62, selectionRect.yMin, 16, 16), overrides.folderIconEditorSprite);

            }
            else
            {
                if (overrides != null && overrides.Pairs != null)
                {
                    foreach (StringIconPair pair in overrides.Pairs)
                    {
                        if (obj.name.Contains(pair.String))
                            icon = pair.Icon;
                    }
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