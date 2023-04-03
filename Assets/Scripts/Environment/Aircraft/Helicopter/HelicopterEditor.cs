using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

# if UNITY_EDITOR
[CustomEditor(typeof(Helicopter))]
public class HelicopterEditor : Editor
{
    private Helicopter helicopter;

    private void OnEnable()
    {
        helicopter = serializedObject.targetObject as Helicopter;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(25);

        using (new GUILayout.HorizontalScope())
        {
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.Label($"Time: {helicopter.timeToComplete}", EditorStyles.boldLabel);
                GUILayout.Label($"Distance passed: {helicopter.DistancePassed}", EditorStyles.boldLabel);
                GUILayout.Label($"Route completion: {helicopter.RouteCompletion}", EditorStyles.boldLabel);
            }

            using (new GUILayout.VerticalScope())
            {
                if (GUILayout.Button("Launch", GUILayout.Width(100))) helicopter.Launch();
                if (GUILayout.Button("Stop", GUILayout.Width(100))) helicopter.Stop();
            }
        }
    }
}
#endif
