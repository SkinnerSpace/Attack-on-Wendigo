using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ColoredHierarchy : MonoBehaviour
{
    private static Vector2 offset = new Vector2(0, 2);

    static ColoredHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        Color fontColor = Color.blue;
        Color backgroundColor = new Color(.76f, .76f, .76f);

        var obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj != null)
        {
            var prefabType = PrefabUtility.GetPrefabAssetType(obj);
            if (PrefabUtility.GetPrefabInstanceStatus(obj) != PrefabInstanceStatus.NotAPrefab)
            {
                if (Selection.Contains(instanceID))
                {
                    fontColor = Color.white;
                    backgroundColor = new Color(0.24f, 0.48f, 0.90f);
                }

                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                EditorGUI.DrawRect(selectionRect, backgroundColor);
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle() 
                {
                    normal = new GUIStyleState() { textColor = fontColor },
                    fontStyle = FontStyle.Bold
                }
                );
            }
        }
    }
}
