using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LogoFaceController))]
public class LogoFaceControllerEditor : Editor
{
    private LogoFaceController controller;

    private void OnEnable()
    {
        controller = serializedObject.targetObject as LogoFaceController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Debug.Log("FUCK YOU");

        using (new GUILayout.VerticalScope())
        {
            GUILayout.Space(10);

            if (GUILayout.Button("Idle"))
            {
                controller.SetStage(LogoAnimationStages.Idle);
            }

            if (GUILayout.Button("Pierced"))
            {
                controller.SetStage(LogoAnimationStages.Pierced);
            }

            if (GUILayout.Button("Final"))
            {
                controller.SetStage(LogoAnimationStages.Final);
            }
        }
    }
}

