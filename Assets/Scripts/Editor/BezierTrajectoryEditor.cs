using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
