﻿using System.Collections;
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
            GUILayout.Space(10);

            GUILayout.Label("Visualizer");
            
            using (new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("On"))
                {
                    bezierTrajectory.SwitchOnVisualization();
                }

                if (GUILayout.Button("Off"))
                {
                    bezierTrajectory.SwitchOffVisualization();
                }
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Generate Trajectory")){
                bezierTrajectory.SwitchOnVisualization();
                bezierTrajectory.GenerateTrajectory();
            }

            if (GUILayout.Button("Generate Escape Trajectory")){
                bezierTrajectory.SwitchOnVisualization();
                bezierTrajectory.GenerateEscapeTrajectroy();
            }

            if (GUILayout.Button("Cancel")){
                bezierTrajectory.SwitchOffVisualization();
            }
        }
    }
}
# endif
