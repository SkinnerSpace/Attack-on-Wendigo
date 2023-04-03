using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

# if UNITY_EDITOR
[CustomEditor(typeof(BezierTrajectory))]
public class BezierTrajectoryEditor : Editor
{
    private SerializedObject trajectory;
    private BezierTrajectory bezierTrajectory;

    private void OnEnable()
    {
        trajectory = serializedObject;
        bezierTrajectory = trajectory.targetObject as BezierTrajectory;
    }

    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(50);
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Generate Trajectory"))
                {
                    bezierTrajectory.GenerateTrajectory();
                }
            }
        }
    }
}
# endif
